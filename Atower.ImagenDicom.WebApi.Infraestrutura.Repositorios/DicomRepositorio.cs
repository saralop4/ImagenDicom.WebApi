using Atower.ImagenDicom.WebApi.Dominio.Interfaces;
using Atower.ImagenDicom.WebApi.Dominio.Modelos;
using FellowOakDicom.Network.Client;
using FellowOakDicom.Network;
using FellowOakDicom;
using Microsoft.Extensions.Configuration;

namespace Atower.ImagenDicom.WebApi.Infraestrutura.Repositorios;

public class DicomRepositorio : IDicomRepositorio
{
    private readonly IConfiguration _config;
    private readonly IDicomClientFactory _dicomClientFactory;

    public DicomRepositorio(IConfiguration config, IDicomClientFactory dicomClientFactory)
    {
        _config = config;
        _dicomClientFactory = dicomClientFactory;
    }

    public async Task<DicomImage?> GetDicomImageAsync(string patientId, CancellationToken cancellationToken)
    {
        var pacsServerIP = _config["PacsServer:ServerIP"];
        var pacsServerPort = int.Parse(_config["PacsServer:ServerPort"]);
        var yourAETitle = _config["PacsServer:AET"];
        var pacsAETitle = _config["PacsServer:CalledAET"];

        //Crear Cliente DICOM
        var client = _dicomClientFactory.Create(pacsServerIP, pacsServerPort, false, yourAETitle, pacsAETitle);

        //Se crea y envía una solicitud C-FIND para buscar estudios de un paciente por su ID.
        var cfindRequest = DicomCFindRequest.CreatePatientQuery(patientId: patientId);
        DicomCFindResponse? response = null;

        //OnResponseReceived Evento que captura las respuestas de la solicitud.
        cfindRequest.OnResponseReceived = (req, res) =>
        {
            response = res;
            Console.WriteLine("Study UID: {0}", req.Dataset.GetString(DicomTag.StudyInstanceUID));
        };

        await client.AddRequestAsync(cfindRequest);

        /*ermite cancelar operaciones asincrónicas. 
         * Es útil para gestionar la finalización anticipada de 
         * tareas en escenarios donde se necesita abortar operaciones 
         * largas o cuando el usuario decide cancelar la acción en curso, 
         * por ejemplo, cerrando la aplicación o cancelando una operación 
         * desde la interfaz de usuario.
         */
        await client.SendAsync(cancellationToken);


        //Comprobar Respuesta y Enviar Solicitud de Almacenamiento (C-STORE)
        if (response?.Status == DicomStatus.Success)
        {
            var studyUid = response.Dataset.GetString(DicomTag.StudyInstanceUID);
            var filePath = "path/to/save/image.dcm";

            var cstoreRequest = new DicomCStoreRequest(filePath);
            await client.AddRequestAsync(cstoreRequest);
            await client.SendAsync(cancellationToken);

            var dicomImage = new FellowOakDicom.Imaging.DicomImage(filePath);
            byte[] imageData = dicomImage.RenderImage().As<byte[]>();

            return new DicomImage(studyUid, filePath, imageData);

            /*Si la respuesta es exitosa, se obtiene el UID del estudio y 
             * se envía una solicitud C-STORE para guardar la imagen en un archivo.
             * Luego se carga y renderiza la imagen DICOM.
             */
        }

        return null;
    }
}

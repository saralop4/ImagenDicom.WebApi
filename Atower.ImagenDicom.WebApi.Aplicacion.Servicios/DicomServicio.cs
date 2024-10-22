using Atower.ImagenDicom.WebApi.Aplicacion.Interfaces;
using Atower.ImagenDicom.WebApi.Dominio.Interfaces;
using Atower.ImagenDicom.WebApi.Dominio.Modelos;

namespace Atower.ImagenDicom.WebApi.Aplicacion.Servicios;

public class DicomServicio : IDicomServicio
{
    private readonly IDicomRepositorio _dicomRepositorio;

    public DicomServicio(IDicomRepositorio dicomRepositorio)
    {
        _dicomRepositorio = dicomRepositorio;
    }

    public async Task<DicomImage?> GetDicomImageAsync(string patientId, CancellationToken cancellationToken)
    {
        return await _dicomRepositorio.GetDicomImageAsync(patientId, cancellationToken);
    }
}

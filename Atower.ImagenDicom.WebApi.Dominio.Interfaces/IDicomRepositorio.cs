using Atower.ImagenDicom.WebApi.Dominio.Modelos;

namespace Atower.ImagenDicom.WebApi.Dominio.Interfaces;

public interface IDicomRepositorio
{
    Task<DicomImage?> GetDicomImageAsync(string patientId, CancellationToken cancellationToken);
   // Task SaveDicomImageAsync(byte[] image);
}

using Atower.ImagenDicom.WebApi.Dominio.Modelos;

namespace Atower.ImagenDicom.WebApi.Dominio.Interfaces;

public interface IDicomRepository
{
    Task<DicomImage> GetDicomImageByIdAsync(string id);
    Task SaveDicomImageAsync(DicomImage dicomImage);
}

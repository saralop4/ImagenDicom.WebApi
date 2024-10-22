using Atower.ImagenDicom.WebApi.Aplicacion.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Atower.ImagenDicom.WebApi.Controllers
{
    [Route("Api/[controller]")]
    [ApiController]
    public class DicomController : ControllerBase
    {
        private readonly IDicomServicio _dicomService;

        public DicomController(IDicomServicio dicomService)
        {
            _dicomService = dicomService;
        }

        [HttpGet("GetDicomImage/{patientId}")]
        public async Task<IActionResult> GetDicomImage(string patientId, CancellationToken cancellationToken)
        {
            var dicomImage = await _dicomService.GetDicomImageAsync(patientId, cancellationToken);

            if (dicomImage != null)
            {
                return File(dicomImage.ImageData, "image/jpeg");

                // En lugar de devolver un JPEG, puedes devolver el archivo DICOM:
                //return File(System.IO.File.ReadAllBytes("path/to/save/image.dcm"), "application/dicom");

                // return File(dicomImage.ImageData, "application/dicom"
            }

            return NotFound("No se encontró la imagen DICOM.");
           
        }
    }
}
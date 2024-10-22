namespace Atower.ImagenDicom.WebApi.Dominio.Modelos
{
    public class DicomImage
    {
        public string StudyInstanceUID { get; set; }
        public string FilePath { get; set; }
        public byte[] ImageData { get; set; }

        public DicomImage(string studyInstanceUID, string filePath, byte[] imageData)
        {
            StudyInstanceUID = studyInstanceUID;
            FilePath = filePath;
            ImageData = imageData;
        }
    }
}

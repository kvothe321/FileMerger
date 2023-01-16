namespace FileMerger.Model
{
    public class FileModel
    {
        public string? FileNameWithPath { get; set; }
        public string? FileName { get; set; }
        public bool IsSelected { get; set; }
        public string? FileExtension { get; set; }
    }
}

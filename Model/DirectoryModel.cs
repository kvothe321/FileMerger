using System.Collections.Generic;

namespace FileMerger.Model
{
    public class DirectoryModel
    {
        public string? DirectoryNameWithPath { get; set; }
        public string? DirectoryName { get; set; }
        public List<DirectoryModel> SubDirectories { get; set; } = new List<DirectoryModel>();
        public List<FileModel> Files { get; set; } = new List<FileModel>();
    }
}

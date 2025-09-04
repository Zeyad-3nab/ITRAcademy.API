namespace ITRProject.API.PL.Helpers
{
    public static class DocumentSettings
    {
        public static string Upload(IFormFile file, string FolderName)
        {
            //string FolderPath=Path.Combine(Directory.GetCurrentDirectory(), )

            //1- Get Folder Name in wwwroot
            string FolderPath = Path.Combine($"{Directory.GetCurrentDirectory()}", $"wwwroot\\{FolderName}");

            // set folder name as a unique using guid
            string FileName = $"{Guid.NewGuid()}{file.FileName}";

            // Get File Path FolderPath + FolderName
            string FilePath = Path.Combine(FolderPath, FileName);

            //FileStream because copy to take file stream
            using var FileStream = new FileStream(FilePath, FileMode.Create);

            //set image in wwwroot
            file.CopyTo(FileStream);

            return FileName;
        }


        public static void Delete(string ImageName, string FolderName)
        {
            // Get Image Name from wwwroot
            string FilePath = Path.Combine($"{Directory.GetCurrentDirectory()}", $"wwwroot\\{FolderName}", $"{ImageName}");


            if (File.Exists(FilePath))
                File.Delete(FilePath);
        }
    }
}

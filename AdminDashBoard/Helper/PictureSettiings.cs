namespace AdminDashBoard.Helper
{
	public class PictureSettiings
	{

		public static string UploadFile(IFormFile file,string folderName)
		{
			//1.Get folder Path
			var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images", folderName);

			//2.set file Name to be unique

			var fileName = Guid.NewGuid() + file.FileName;

			//get file Path

			var filePath=Path.Combine(folderPath, fileName);

			//4.save file as streeam

			var fs=new FileStream(filePath,FileMode.Create);

			file.CopyTo(fs);


			return Path.Combine("images//products", fileName);
		}


		public static void DeleteFile(string folderName,string fileName) 
		{
			var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images", folderName,fileName);

			if(File.Exists(filePath))
				File.Delete(filePath);

		}




	}
}

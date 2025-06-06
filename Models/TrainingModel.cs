namespace dotnet_stocks_api.Models
{
    public class TrainingModel
    {
        public int TrainingId {get; set;}
        public string TrainingName {get; set;}

        public List<StudentModel> Students {get; set;}
    }
}
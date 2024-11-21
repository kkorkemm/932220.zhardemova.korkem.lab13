using System.ComponentModel.DataAnnotations;

public class QuizQuestion
{
    public string Expression { get; set; }

    [Required(ErrorMessage = "Answer is required")]
    public int Answer { get; set; }

    [Required(ErrorMessage = "User answer is required")]
    public int UserAnswer { get; set; }

    public bool IsCorrect => Answer == UserAnswer;
}
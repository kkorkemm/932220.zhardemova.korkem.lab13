using System.Collections.Generic;

namespace WebApplication1.Models
{
    public class QuizViewModel
    {
        public List<QuizQuestion> Questions { get; set; } = new List<QuizQuestion>();
        public int CurrentQuestionIndex { get; set; } = 0;
    }
}
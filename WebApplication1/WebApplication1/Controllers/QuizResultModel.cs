using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Xml.XPath;
using WebApplication1.Models;

public class QuizResultModel : PageModel
{
    public QuizViewModel ViewModel { get; set; }

    public void OnGet(List<QuizQuestion> expressions)
    {
        ViewModel = new QuizViewModel
        {
            Questions = expressions
        };
    }
}
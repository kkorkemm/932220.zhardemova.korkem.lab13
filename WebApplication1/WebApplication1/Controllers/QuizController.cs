using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class QuizController : Controller
    {
        private static QuizViewModel _quizViewModel = new QuizViewModel();

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Quiz()
        {
            if (_quizViewModel.Questions.Count == 0 || _quizViewModel.CurrentQuestionIndex >= _quizViewModel.Questions.Count)
            {
                _quizViewModel.Questions.Add(GenerateQuestion());
                _quizViewModel.CurrentQuestionIndex = 0;
            }

            return View(_quizViewModel.Questions[_quizViewModel.CurrentQuestionIndex]);
        }

        [HttpPost]
        public IActionResult Quiz(int userAnswer, string action)
        {
            if (ModelState.IsValid)
            {
                _quizViewModel.Questions[_quizViewModel.CurrentQuestionIndex].UserAnswer = userAnswer;

                if (action == "Next")
                {
                    _quizViewModel.CurrentQuestionIndex++;

                    // Добавляем новый вопрос, если текущий индекс равен количеству вопросов
                    if (_quizViewModel.CurrentQuestionIndex == _quizViewModel.Questions.Count)
                    {
                        _quizViewModel.Questions.Add(GenerateQuestion());
                    }
                }
                else if (action == "Finish")
                {
                    // Сохраняем последний ответ перед переходом к результатам
                    _quizViewModel.Questions[_quizViewModel.CurrentQuestionIndex].UserAnswer = userAnswer;
                    return RedirectToAction("QuizResult");
                }

                return RedirectToAction("Quiz");
            }

            return View(_quizViewModel.Questions[_quizViewModel.CurrentQuestionIndex]);
        }

        public IActionResult QuizResult()
        {
            int correctAnswers = _quizViewModel.Questions.Count(q => q.IsCorrect);
            ViewBag.CorrectAnswers = correctAnswers;
            ViewBag.TotalQuestions = _quizViewModel.Questions.Count;
            return View(_quizViewModel.Questions);
        }

        private QuizQuestion GenerateQuestion()
        {
            Random random = new Random();
            int num1 = random.Next(1, 100);
            int num2 = random.Next(1, 100);
            string operation = random.Next(4) switch
            {
                0 => "+",
                1 => "-",
                2 => "*",
                3 => "/",
                _ => "+"
            };

            int answer = operation switch
            {
                "+" => num1 + num2,
                "-" => num1 - num2,
                "*" => num1 * num2,
                "/" => num1 / num2,
                _ => num1 + num2
            };

            return new QuizQuestion
            {
                Expression = $"{num1} {operation} {num2}",
                Answer = answer
            };
        }
    }
}
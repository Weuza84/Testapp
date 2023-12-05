using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using Testapp.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Testapp.ViewModels
{
    [QueryProperty(nameof(QuestionId), nameof(QuestionId))]
    public class QuestionViewModel : BaseViewModel
    {
        private string questionId;
        private string text;
        private string imageUrl;
        private string selectedRaum;
        private string selectedPosition;
        private Question selectedQuestion;
        private Constraint xContraint;
        private Constraint yContraint;
        private Constraint heightConstraint;
        private Constraint widthConstraint;
        private Constraint rightConstraint;
        private Constraint bottomConstraint;
        private string idInput;
        private bool isVisRollo;
        private Color resultColor;

        private ObservableCollection<Question> questionList;

        public Command NextQuestion { get; }
        public Command<string> OnGeraeteRaumChanged { get; }
        public Command<string> TapGestureRecognizer { get; }
        public Command ImageGestureRecognizer { get; }

        public int attemps { get; set; }

        public string ID
        {
            get => idInput;
            set => SetProperty(ref idInput, value);
        }

        public bool IsVisRollo
        {
            get => isVisRollo;
            set => SetProperty(ref isVisRollo, value);
        }

        public string Text
        {
            get => text;
            set => SetProperty(ref text, value);
        }

        public string ImageUrl
        {
            get => imageUrl;
            set => SetProperty(ref imageUrl, value);
        }

        public Constraint XConstraint
        {
            get => xContraint;
            set => SetProperty(ref xContraint, value);
        }

        public Constraint YConstraint
        {
            get => yContraint;
            set => SetProperty(ref yContraint, value);
        }

        public Constraint WidthConstraint
        {
            get => widthConstraint;
            set => SetProperty(ref widthConstraint, value);
        }

        public Constraint HeightConstraint
        {
            get => heightConstraint;
            set => SetProperty(ref heightConstraint, value);
        }

        public Constraint RightConstraint
        {
            get => rightConstraint;
            set => SetProperty(ref rightConstraint, value);
        }

        public Constraint BottomConstraint
        {
            get => bottomConstraint;
            set => SetProperty(ref bottomConstraint, value);
        }

        public Color ResultColor
        {
            get => resultColor;
            set => SetProperty(ref resultColor, value);
        }

        public string QuestionId
        {
            get
            {
                return questionId;
            }
            set
            {
                questionId = value;
                LoadQuestionId(value);
            }
        }

        public Question SelectedQuestion
        {
            get
            {
                return selectedQuestion;
            }

            set
            {
                selectedQuestion = value;
            }
        }

        public ObservableCollection<Question> QuestionList
        {
            get
            {
                return questionList;
            }

            set
            {
                questionList = value;
            }
        }

        public string SelectedRaum
        {
            get
            {
                return selectedRaum;
            }

            set
            {
                selectedRaum = value;
            }
        }

        public string SelectedPosition
        {
            get
            {
                return selectedPosition;
            }

            set
            {
                selectedPosition = value;
            }
        }


        public QuestionViewModel()
        {
            QuestionList = new ObservableCollection<Question>();
            NextQuestion = new Command(NextQuestionCommand);
            //OnGeraeteRaumChanged = new Command<string>(GetGeraeteRaum);
            TapGestureRecognizer = new Command<string>(Topcommand);
            ImageGestureRecognizer = new Command(ImageCommand);
        }

        public async void LoadQuestionId(string itemId)
        {
            try
            {
                List<Question> questList = await App.Database.GetQuestionsAsync();

                if (questList.Count <= 0)
                {
                    questList = await App.DataManager.GetQuestionsAsync();

                    foreach (Question quest in questList)
                    {
                        JObject jsonObj = JObject.Parse(quest.position);
                        Dictionary<string, double> dict = jsonObj.ToObject<Dictionary<string, double>>();

                        quest.xConstraint = Constraint.RelativeToParent((parent) => { return parent.Width * dict["x"]; });
                        quest.yConstraint = Constraint.RelativeToParent((parent) => { return parent.Height * dict["y"]; });
                        quest.heightConstraint = Constraint.RelativeToParent((parent) => { return parent.Height * dict["h"]; });
                        quest.widthConstraint = Constraint.RelativeToParent((parent) => { return parent.Width * dict["w"]; });
                        quest.rightConstraint = Constraint.RelativeToParent((parent) => { return parent.Width * (dict["x"] + dict["w"]); });
                        quest.bottomConstraint = Constraint.RelativeToParent((parent) => { return parent.Height * (dict["y"] + dict["h"]); });

                        await App.Database.SaveQuestionAsync(quest);
                    }
                }

                questList = await App.Database.GetQuestionsByModulAsync(int.Parse(itemId));

                foreach (Question quest in questList)
                {
                    JObject jsonObj = JObject.Parse(quest.position);
                    Dictionary<string, double> dict = jsonObj.ToObject<Dictionary<string, double>>();

                    quest.xConstraint = Constraint.RelativeToParent((parent) => { return parent.Width * dict["x"]; });
                    quest.yConstraint = Constraint.RelativeToParent((parent) => { return parent.Height * dict["y"]; });
                    quest.heightConstraint = Constraint.RelativeToParent((parent) => { return parent.Height * dict["h"]; });
                    quest.widthConstraint = Constraint.RelativeToParent((parent) => { return parent.Width * dict["w"]; });
                    quest.rightConstraint = Constraint.RelativeToParent((parent) => { return parent.Width * (dict["x"] + dict["w"]); });
                    quest.bottomConstraint = Constraint.RelativeToParent((parent) => { return parent.Height * (dict["y"] + dict["h"]); });

                    QuestionList.Add(quest);
                }

                Module modul = await App.Database.GetModuleAsync(int.Parse(itemId));

                Title = modul.Name;

                var path = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                var folder = Path.Combine(path, "FF_Pics"); ;
                var file = Path.Combine(folder, modul.Pic);

                ImageUrl = file;

                ChooseNewQuestion();

            }
            catch (Exception ex)
            {
                Debug.WriteLine("Failed to Load Item");
            }
        }

        public void ChooseNewQuestion()
        {
            IsVisRollo = true;
            Random rnd = new Random();
            int questionNumber = rnd.Next(0, QuestionList.Count);
            SelectedQuestion = QuestionList[questionNumber];

            ID = SelectedQuestion.Id.ToString();
            Text = SelectedQuestion.Text;
            XConstraint = SelectedQuestion.xConstraint;
            YConstraint = SelectedQuestion.yConstraint;
            HeightConstraint = SelectedQuestion.heightConstraint;
            WidthConstraint = SelectedQuestion.widthConstraint;
            RightConstraint = SelectedQuestion.rightConstraint;
            BottomConstraint = SelectedQuestion.bottomConstraint;
            ResultColor = Color.Transparent;
        }

        async void NextQuestionCommand()
        {
            ChooseNewQuestion();
        }

        async void GetGeraeteRaum(string selectedRaum)
        {
            SelectedRaum = selectedRaum;
        }

        void Topcommand(string selectedPosition)
        {
            SelectedPosition = selectedPosition;

            if (SelectedQuestion.Id.ToString().Equals(SelectedPosition))
            {
                ResultColor = Color.DarkGreen;
                IsVisRollo = false;
                attemps = 0;
            }
        }

        void ImageCommand()
        {
            attemps = attemps + 1;

            if (attemps > 3)
            {
                IsVisRollo = false;
                attemps = 0;
                ResultColor = Color.Red;
            }
        }
    }
}

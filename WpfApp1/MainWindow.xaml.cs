using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Windows;

namespace WpfApp1
{
    public class MainViewModel : ViewModelBase
    {
        private DateTime _birthDate;
        private string _ageResult;
        private string _birthdayMessage;
        private string _westernZodiacSign;
        private string _chineseZodiacSign;

        public DateTime BirthDate
        {
            get { return _birthDate; }
            set { Set(ref _birthDate, value); }
        }

        public string AgeResult
        {
            get { return _ageResult; }
            set { Set(ref _ageResult, value); }
        }

        public string BirthdayMessage
        {
            get { return _birthdayMessage; }
            set { Set(ref _birthdayMessage, value); }
        }

        public string WesternZodiacSign
        {
            get { return _westernZodiacSign; }
            set { Set(ref _westernZodiacSign, value); }
        }

        public string ChineseZodiacSign
        {
            get { return _chineseZodiacSign; }
            set { Set(ref _chineseZodiacSign, value); }
        }

        public RelayCommand CalculateCommand { get; private set; }

        public MainViewModel()
        {
            CalculateCommand = new RelayCommand(Calculate);
        }

        private void Calculate()
        {
            int? age;
             try {
                //Calculate age
                age = CalculateAge();

           } catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;

            }


            if (age.HasValue)
            {
                AgeResult = $"Your age is: {age} years";
            }

            //Check birthday
            CheckBirthday();

            //Calculate zodiac signs
            CalculateZodiacSigns();
        }

        //Calculate Age method
        private int? CalculateAge()
        {

                DateTime today = DateTime.Today;
                int age = today.Year - BirthDate.Year;//the age of the user

                if (BirthDate > today.AddYears(-age))
                {
                    age--;
                }


            //Check if age is within a reasonable range
            if (age < 0 || age > 135)
            {
                throw new Exception("Invalid age. Please check your date of birth.");

            }

            return age;
            
          
        }

        //Check for birthday method
        private void CheckBirthday()
        {
            if (BirthDate.Month == DateTime.Today.Month && BirthDate.Day == DateTime.Today.Day)
            {
                BirthdayMessage = "Happy Birthday!";
            }
            else
            {
                BirthdayMessage = string.Empty;
            }
        }

        //calculate zodiac signs method
        private void CalculateZodiacSigns()
        {
            
            //Use user's birth month for the Western sign
            _westernZodiacSign = GetWesternZodiacSign(BirthDate.Month);

            //Use user's birth year for the Chinese sign
            _chineseZodiacSign = GetChineseZodiacSign(BirthDate.Year);

            RaisePropertyChanged(nameof(WesternZodiacSign));
            RaisePropertyChanged(nameof(ChineseZodiacSign));
        }

        //Wester sign calculator
        private string GetWesternZodiacSign(int month)
        {
            
            //Try to replace with actual logic?
            switch (month)
            {
                case 1: return "Capricorn";
                case 2: return "Aquarius";
                // ... 
                default: return "Unknown";
            }
        }

        //Chinese sign calculator
        private string GetChineseZodiacSign(int year)
        {
            
            //Try to replace with actual logic?
            string[] chineseZodiacSigns = { "Rat", "Ox", "Tiger", "Rabbit", "Dragon", "Snake", "Horse", "Goat", "Monkey", "Rooster", "Dog", "Pig" };
            int index = (year - 1900) % 12;
            return chineseZodiacSigns[index];
        }
    }
}

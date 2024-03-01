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
            _westernZodiacSign = GetWesternZodiacSign(BirthDate.Month, BirthDate.Day);

            //Use user's birth year for the Chinese sign
            _chineseZodiacSign = GetChineseZodiacSign(BirthDate.Year);

            RaisePropertyChanged(nameof(WesternZodiacSign));
            RaisePropertyChanged(nameof(ChineseZodiacSign));
        }

        //Wester sign calculator
        private string GetWesternZodiacSign(int month, int day)
        {
            string str = string.Empty;

            //JAN-FEB
            if (((month == 1) && (day >= 23 && day <= 31)) || ((month == 2) && (day >= 01 && day <= 21)))
            {
               return str = "Aquarius";
            }
            //FEB-MAR
            if (((month == 2) && (day >= 23 && day <= 28)) || ((month == 3) && (day >= 01 && day <= 21)))
            {
                return str = "Pisces";
            }
            //MAR-APR
            if (((month == 3) && (day >= 21 && day <= 31)) || ((month == 4) && (day >= 01 && day <= 20)))
            {
                return str = "Aires";
            }
            //APR-MAY
            if (((month == 4) && (day >= 21 && day <= 31)) || ((month == 5) && (day >= 01 && day <= 21)))
            {
                return str = "Taurus";
            }
            //MAY-JUN
            if (((month == 5) && (day >= 21 && day <= 31)) || ((month == 6) && (day >= 01 && day <= 21)))
            {
                return str = "Gemini";
            }
            //JUN-JULY
            if (((month == 6) && (day >= 22 && day <= 31)) || ((month == 7) && (day >= 01 && day <= 22)))
            {
                return str = "Cancer";
            }
            //JUL-AUG
            if (((month == 7) && (day >= 23 && day <= 31)) || ((month == 8) && (day >= 01 && day <= 22)))
            {
                return str = "Leo";
            }
            //AUG-SEP
            if (((month == 8) && (day >= 23 && day <= 31)) || ((month == 9) && (day >= 01 && day <= 21)))
            {
                return str = "Virgo";
            }
            //SEP-OCT
            if (((month == 9) && (day >= 23 && day <= 31)) || ((month == 10) && (day >= 01 && day <= 21)))
            {
                return str = "Libra";
            }
            //OCT-NOV
            if (((month == 10) && (day >= 23 && day <= 31)) || ((month == 11) && (day >= 01 && day <= 21)))
            {
                return str = "Scorpio";
            }
            //NOV-DEC
            if (((month == 11) && (day >= 23 && day <= 31)) || ((month == 12) && (day >= 01 && day <= 21)))
            {
                return str = "Sagittarius";
            }
            //DEC-JAN
            if (((month == 12) && (day >= 23 && day <= 31)) || ((month == 1) && (day >= 01 && day <= 21)))
            {
                return str = "Capricorn";
            }
            return str = "Unknown";
          
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

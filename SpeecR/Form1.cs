using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Speech.Recognition;

namespace SpeecR
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        static Label l;

        static void sre_SpechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            //Result.Confidence  это степень распознования
            if (e.Result.Confidence > 0.82)
            {
                l.Text = e.Result.Text;
                l.Text += "\t\n";
            }
        }

        // событие при отрисовке формы
        private void Form1_Shown(object sender, EventArgs e)
        {
            l = label1; // для вывода распоз слов

            // указываем какой язык требуется распозновать
            System.Globalization.CultureInfo ci = new System.Globalization.CultureInfo("ru-ru");
          
            // обьект для распознования речи. 
            SpeechRecognitionEngine sre = new SpeechRecognitionEngine(ci);
          
            // место откуда берется звуковой источник. В данном случаее источник по умолчанию.(микрофон)
            sre.SetInputToDefaultAudioDevice();

            //обработчик события распознования текста. Распознает текст и вызывает метод sre_SpechRecognized
            sre.SpeechRecognized += new EventHandler<SpeechRecognizedEventArgs>(sre_SpechRecognized);


            Choices numbers = new Choices();

            numbers.Add( new string[] {"один", "два", "три", "четыре", "пять","алло", "прием"," 1c","база", "так" });

            GrammarBuilder gb = new GrammarBuilder();
            //  gb.Culture = ci;
            gb.Append(numbers);


            Grammar g = new Grammar(gb);
            sre.LoadGrammar(g);

            //количество распознаных слов
            sre.RecognizeAsync(RecognizeMode.Multiple);
        }
    }
}

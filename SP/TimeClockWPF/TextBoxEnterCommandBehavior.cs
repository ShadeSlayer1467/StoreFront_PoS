using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows;

namespace TimeClockWPF
{
    public static class TextBoxEnterCommandBehavior
    {
        public static readonly DependencyProperty EnterCommandProperty =
            DependencyProperty.RegisterAttached("EnterCommand", typeof(ICommand), typeof(TextBoxEnterCommandBehavior), new PropertyMetadata(null, EnterCommandChanged));

        public static ICommand GetEnterCommand(TextBox textBox)
        {
            return (ICommand)textBox.GetValue(EnterCommandProperty);
        }

        public static void SetEnterCommand(TextBox textBox, ICommand value)
        {
            textBox.SetValue(EnterCommandProperty, value);
        }

        private static void EnterCommandChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var textBox = (TextBox)d;

            if (e.OldValue == null && e.NewValue != null)
            {
                textBox.KeyUp += TextBoxKeyUp;
            }
            else if (e.OldValue != null && e.NewValue == null)
            {
                textBox.KeyUp -= TextBoxKeyUp;
            }
        }

        private static void TextBoxKeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                var textBox = (TextBox)sender;
                var command = GetEnterCommand(textBox);

                if (command != null && command.CanExecute(textBox.Text))
                {
                    command.Execute(textBox.Text);
                }
            }
        }
    }
}

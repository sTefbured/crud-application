﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace Lab1
{
    class LimitedTextBox : TextBox
    {
        public LimitedTextBox()
        {
            KeyPress += keyPress;
        }

        private void keyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != '\b')
            {
                e.Handled = true;
            }
        }
    }
}
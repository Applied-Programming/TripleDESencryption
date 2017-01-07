﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Security.Cryptography;

namespace TripleDESencryption
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        //Encryption

        byte[] encrypted;
        private void button1_Click(object sender, EventArgs e)
        {
            MD5CryptoServiceProvider md5_sp = new MD5CryptoServiceProvider();
            UTF8Encoding utf8 = new UTF8Encoding();
            TripleDESCryptoServiceProvider tdes_sp = new TripleDESCryptoServiceProvider();
            tdes_sp.Key = md5_sp.ComputeHash(utf8.GetBytes(textBox1.Text));
            tdes_sp.Mode = CipherMode.ECB;
            tdes_sp.Padding = PaddingMode.PKCS7;
            ICryptoTransform trans = tdes_sp.CreateEncryptor();
            encrypted = trans.TransformFinalBlock(utf8.GetBytes(textBox2.Text), 0, utf8.GetBytes(textBox2.Text).Length);
            textBox3.Text = BitConverter.ToString(encrypted);
        }

        //Decryption
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                MD5CryptoServiceProvider md5_sp = new MD5CryptoServiceProvider();
                UTF8Encoding utf8 = new UTF8Encoding();
                TripleDESCryptoServiceProvider tdes_sp = new TripleDESCryptoServiceProvider();
                tdes_sp.Key = md5_sp.ComputeHash(utf8.GetBytes(textBox5.Text));
                tdes_sp.Mode = CipherMode.ECB;
                tdes_sp.Padding = PaddingMode.PKCS7;
                ICryptoTransform trans = tdes_sp.CreateDecryptor();
                textBox4.Text = utf8.GetString(trans.TransformFinalBlock(encrypted, 0, encrypted.Length));
            }
            catch { MessageBox.Show("Please try again. The keys do not match!"); }
        }
    }
}

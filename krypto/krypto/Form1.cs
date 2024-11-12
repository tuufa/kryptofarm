using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
//using Microsoft.Win32;
using SaveFileDialog = System.Windows.Forms.SaveFileDialog;
using System.Security.Cryptography;
using System.Numerics;

namespace krypto
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }





        private void button1_Click(object sender, EventArgs e)
        {
            // Получаем текст из textBox1
            string inputText = textBox1.Text;

            // Генерируем случайный ключ такой же длины, как и текст
            string key = GenerateRandomKey(inputText.Length);

            // Шифруем текст с использованием шифра Вернама
            string encryptedText = VernamEncrypt(inputText, key);

            // Дешифруем текст с использованием шифра Вернама
            string decryptedText = VernamDecrypt(encryptedText, key);

            // Выводим зашифрованный текст в label1
            label1.Text = $"Зашифрованный текст(Шифр Вернома): {encryptedText}\nКлюч: {key}";


            // Выводим расшифрованный текст в label5
            label5.Text = $"Расшифрованный текст: {decryptedText}";

            // Вызов диалогового окна для выбора пути сохранения файла
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.FileName = "Vernam.txt"; // Имя файла по умолчанию
                saveFileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
                saveFileDialog.Title = "Сохранить зашифрованный текст";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string filePath = saveFileDialog.FileName;

                    try
                    {
                        // Запись зашифрованного текста и ключа в выбранный файл
                        File.WriteAllText(filePath, $"Зашифрованный текст: {encryptedText}\nКлюч: {key}\nРасшифрованный текст: {decryptedText}");
                        MessageBox.Show($"Зашифрованный текст и ключ успешно сохранены в файл: {filePath}", "Сохранение успешно", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка при сохранении файла: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Сохранение отменено.", "Отмена", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }


        }        
        
        private void button9_Click(object sender, EventArgs e)
        {
            // Создаем диалог для выбора файла
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";

            // Проверяем, выбрал ли пользователь файл
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                // Читаем текст из выбранного файла
                string inputText = System.IO.File.ReadAllText(openFileDialog.FileName);

                // Генерируем случайный ключ такой же длины, как и текст
                string key = GenerateRandomKey(inputText.Length);

                // Шифруем текст с использованием шифра Вернама
                string encryptedText = VernamEncrypt(inputText, key);

                // Дешифруем текст с использованием шифра Вернама
                string decryptedText = VernamDecrypt(encryptedText, key);

                // Выводим зашифрованный текст в label1
                label1.Text = $"Зашифрованный текст (Шифр Вернама): {encryptedText}\nКлюч: {key}";

                // Выводим расшифрованный текст в label5
                label5.Text = $"Расшифрованный текст: {decryptedText}";

                // Вызов диалогового окна для выбора пути сохранения файла
                using (SaveFileDialog saveFileDialog = new SaveFileDialog())
                {
                    saveFileDialog.FileName = "Vernam.txt"; // Имя файла по умолчанию
                    saveFileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
                    saveFileDialog.Title = "Сохранить зашифрованный текст";

                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        string filePath = saveFileDialog.FileName;

                        try
                        {
                            // Запись зашифрованного текста и ключа в выбранный файл
                            File.WriteAllText(filePath, $"Зашифрованный текст: {encryptedText}\nКлюч: {key}\nРасшифрованный текст: {decryptedText}");
                            MessageBox.Show($"Зашифрованный текст и ключ успешно сохранены в файл: {filePath}", "Сохранение успешно", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Ошибка при сохранении файла: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Сохранение отменено.", "Отмена", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }


            }
            else
            {
                MessageBox.Show("Файл не был выбран.");
            }
        }
        // Метод для генерации случайного ключа
        private string GenerateRandomKey(int length)
        {
            var random = new Random();
            var keyBuilder = new StringBuilder();
            for (int i = 0; i < length; i++)
            {
                keyBuilder.Append((char)random.Next(32, 127)); // Символы ASCII от 32 до 126 (включительно)
            }
            return keyBuilder.ToString(); // Возвращаем случайный ключ, состоящий из ASCII-символов
        }

        // Метод для шифрования текста с помощью шифра Вернама
        private string VernamEncrypt(string text, string key)
        {
            var encryptedTextBuilder = new StringBuilder();
            // Цикл по каждому символу текста
            for (int i = 0; i < text.Length; i++)
            {
                // Применяем операцию XOR (исключающее ИЛИ) между символом текста и символом ключа
                char encryptedChar = (char)(text[i] ^ key[i]);
                // Добавляем зашифрованный символ в результат
                encryptedTextBuilder.Append(encryptedChar);
            }
            return encryptedTextBuilder.ToString(); // Возвращаем зашифрованный текст
        }

        // Метод для дешифрования текста с помощью шифра Вернама
        private string VernamDecrypt(string encryptedText, string key)
        {
            var decryptedTextBuilder = new StringBuilder();
            // Цикл по каждому символу зашифрованного текста
            for (int i = 0; i < encryptedText.Length; i++)
            {
                // Применяем операцию XOR (исключающее ИЛИ) между зашифрованным символом и символом ключа
                char decryptedChar = (char)(encryptedText[i] ^ key[i]);
                // Добавляем расшифрованный символ в результат
                decryptedTextBuilder.Append(decryptedChar);
            }
            return decryptedTextBuilder.ToString(); // Возвращаем расшифрованный текст
        }
        /*
            Метод `VernamEncrypt` реализует шифрование текста с помощью побитового шифра Вернама.
            Этот метод использует ключ, который должен быть такой же длины, как и текст.

            Принцип работы шифра Вернама:
            1. Шифрование выполняется побитово: каждый символ исходного текста сравнивается с
               соответствующим символом ключа, и между ними выполняется побитовая операция XOR (исключающее ИЛИ).

            2. XOR — это операция, которая возвращает 1 для каждого бита, если входные биты различны,
               и возвращает 0, если они одинаковы. Например:
               - 0 XOR 0 = 0
               - 1 XOR 1 = 0
               - 0 XOR 1 = 1
               - 1 XOR 0 = 1

            3. Применяя XOR между символами текста и ключа, мы получаем шифрованный символ, который отличается от оригинала.
               Этот зашифрованный символ нельзя расшифровать без знания ключа, что делает метод шифрования стойким.

            4. Для правильной работы метода длина ключа должна совпадать с длиной текста,
               так как каждый символ текста шифруется отдельно, используя соответствующий символ из ключа.

            В результате получается зашифрованный текст, который можно расшифровать тем же методом,
            если использовать тот же ключ, поскольку операция XOR с одинаковыми значениями дважды
            возвращает исходное значение.
        */

        private void button2_Click(object sender, EventArgs e)
        {
            string inputText = textBox1.Text;

            // Генерация случайного ключа и IV
            byte[] key = GenerateRandomKey();
            byte[] iv = GenerateRandomIV();

            string keyString = Convert.ToBase64String(key);
            string ivString = Convert.ToBase64String(iv);

            // Шифрование
            string encryptedText = Encrypt(inputText, key, iv);
            label2.Text = "Зашифрованный текст: " + encryptedText;

            // Дешифрование
            string decryptedText = Decrypt(encryptedText, key, iv);
            label6.Text = "Дешифрованный текст: " + decryptedText;

            // Вызов диалогового окна для выбора пути сохранения файла
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.FileName = "RC2.txt"; // Имя файла по умолчанию
                saveFileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
                saveFileDialog.Title = "Сохранить зашифрованный текст";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string filePath = saveFileDialog.FileName;

                    try
                    {
                        // Запись зашифрованного текста и ключа в выбранный файл
                        File.WriteAllText(filePath, $"Зашифрованный текст: {encryptedText}\nКлюч: {keyString} \n {ivString}\nРасшифрованный текст: {decryptedText}");
                        MessageBox.Show($"Зашифрованный текст и ключ успешно сохранены в файл: {filePath}", "Сохранение успешно", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка при сохранении файла: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Сохранение отменено.", "Отмена", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }


        }        
        
        private void button10_Click(object sender, EventArgs e)
        {
            // Создаем диалог для выбора файла
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";

            // Проверяем, выбрал ли пользователь файл
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                // Читаем текст из выбранного файла
                string inputText = System.IO.File.ReadAllText(openFileDialog.FileName);

                // Генерация случайного ключа и IV
                byte[] key = GenerateRandomKey();
                byte[] iv = GenerateRandomIV();

                string keyString = Convert.ToBase64String(key);
                string ivString = Convert.ToBase64String(iv);

                // Шифрование
                string encryptedText = Encrypt(inputText, key, iv);
                label2.Text = "Зашифрованный текст: " + encryptedText;

                // Дешифрование
                string decryptedText = Decrypt(encryptedText, key, iv);
                label6.Text = "Дешифрованный текст: " + decryptedText;

                // Вызов диалогового окна для выбора пути сохранения файла
                using (SaveFileDialog saveFileDialog = new SaveFileDialog())
                {
                    saveFileDialog.FileName = "RC2.txt"; // Имя файла по умолчанию
                    saveFileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
                    saveFileDialog.Title = "Сохранить зашифрованный текст";

                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        string filePath = saveFileDialog.FileName;

                        try
                        {
                            // Запись зашифрованного текста и ключа в выбранный файл
                            File.WriteAllText(filePath, $"Зашифрованный текст: {encryptedText}\nКлюч: {keyString} \n {ivString}\nРасшифрованный текст: {decryptedText}");
                            MessageBox.Show($"Зашифрованный текст и ключ успешно сохранены в файл: {filePath}", "Сохранение успешно", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Ошибка при сохранении файла: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Сохранение отменено.", "Отмена", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }


            }
            else
            {
                MessageBox.Show("Файл не был выбран.");
            }
        }

        // Генерация случайного ключа для RC2
        private byte[] GenerateRandomKey()
        {
            // Создание объекта RC2 для генерации ключа
            using (RC2 rc2 = RC2.Create())
            {
                rc2.KeySize = 64; // Устанавливаем размер ключа RC2 (64 бита или 8 байт)

                rc2.GenerateKey(); // Генерация случайного ключа на основе выбранного размера
                return rc2.Key; // Возвращаем сгенерированный ключ
            }
        }

        // Генерация случайного инициализационного вектора (IV) для RC2
        private byte[] GenerateRandomIV()
        {
            // Создание объекта RC2 для генерации IV
            using (RC2 rc2 = RC2.Create())
            {
                // Устанавливаем размер IV, который равен размеру блока данных RC2, деленному на 8
                rc2.IV = new byte[rc2.BlockSize / 8]; // Для RC2 размер IV - 8 байт

                rc2.GenerateIV(); // Генерация случайного IV
                return rc2.IV; // Возвращаем сгенерированный IV
            }
        }

        // Шифрование строки с использованием RC2
        private string Encrypt(string plainText, byte[] key, byte[] iv)
        {
            // Создание объекта RC2 для шифрования
            using (RC2 rc2 = RC2.Create())
            {
                // Создаем шифратор на основе ключа и IV
                ICryptoTransform encryptor = rc2.CreateEncryptor(key, iv);

                // Используем память для хранения зашифрованного текста
                using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
                {
                    // Используем CryptoStream для записи зашифрованных данных в поток памяти
                    using (CryptoStream cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                    {
                        // Записываем исходный текст в CryptoStream, он будет зашифрован автоматически
                        using (System.IO.StreamWriter sw = new System.IO.StreamWriter(cs))
                        {
                            sw.Write(plainText); // Записываем исходный текст в поток
                        }
                    }
                    // Возвращаем зашифрованные данные в виде строки Base64 (для удобства отображения)
                    return Convert.ToBase64String(ms.ToArray());
                }
            }
        }

        // Дешифрование строки с использованием RC2
        private string Decrypt(string encryptedText, byte[] key, byte[] iv)
        {
            // Создание объекта RC2 для дешифрования
            using (RC2 rc2 = RC2.Create())
            {
                // Создаем дешифратор на основе ключа и IV
                ICryptoTransform decryptor = rc2.CreateDecryptor(key, iv);

                // Создаем поток памяти из зашифрованных данных (строки Base64)
                using (System.IO.MemoryStream ms = new System.IO.MemoryStream(Convert.FromBase64String(encryptedText)))
                {
                    // Используем CryptoStream для чтения дешифрованных данных из потока
                    using (CryptoStream cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                    {
                        // Читаем дешифрованные данные из потока и возвращаем результат
                        using (System.IO.StreamReader sr = new System.IO.StreamReader(cs))
                        {
                            return sr.ReadToEnd(); // Возвращаем дешифрованный текст
                        }
                    }
                }
            }
        }
        /*
          Метод шифрования с использованием алгоритма RC2:

          1. RC2 - это симметричный блочный шифр, который использует ключ фиксированной длины (в данном случае 64 бита / 8 байт).
          2. Процесс шифрования включает создание объекта RC2 и его настроек, таких как ключ и инициализационный вектор (IV).

          3. В методе:
             - Генерируются случайные ключ и IV с помощью метода `GenerateRandomKey` и `GenerateRandomIV` соответственно.
             - Создается шифратор с помощью метода `CreateEncryptor`, который принимает ключ и IV для шифрования данных.

          4. Затем исходный текст (plainText) передается в поток шифрования:
             - Используется `CryptoStream` для шифрования текста. Данные записываются в поток памяти `MemoryStream`.
             - В процессе записи, `StreamWriter` преобразует текст в поток, автоматически шифруя его через `CryptoStream`.

          5. Зашифрованный результат возвращается как строка в формате Base64, чтобы он мог быть безопасно передан или сохранен.

          6. Процесс шифрования гарантирует, что текст будет преобразован в последовательность зашифрованных байтов, которая будет непригодной для прочтения без знания ключа и IV.

        */
        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            string inputText = textBox1.Text;

            // Генерация случайного ключа для RC4
            byte[] key = GenerateRandomKeyForRC4();

            string keyString = Convert.ToBase64String(key);

            // Шифрование
            string encryptedText = EncryptRC4(inputText, key);
            label3.Text = "Зашифрованный текст (RC4): " + encryptedText;

            // Дешифрование
            string decryptedText = DecryptRC4(encryptedText, key);
            label7.Text = "Дешифрованный текст (RC4): " + decryptedText;

            // Вызов диалогового окна для выбора пути сохранения файла
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.FileName = "RC4.txt"; // Имя файла по умолчанию
                saveFileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
                saveFileDialog.Title = "Сохранить зашифрованный текст";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string filePath = saveFileDialog.FileName;

                    try
                    {
                        // Запись зашифрованного текста и ключа в выбранный файл
                        File.WriteAllText(filePath, $"Зашифрованный текст: {encryptedText}\nКлюч: {keyString}\nРасшифрованный текст: {decryptedText}");
                        MessageBox.Show($"Зашифрованный текст и ключ успешно сохранены в файл: {filePath}", "Сохранение успешно", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка при сохранении файла: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Сохранение отменено.", "Отмена", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }



        }

        private void button11_Click(object sender, EventArgs e)
        {
            // Создаем диалог для выбора файла
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";

            // Проверяем, выбрал ли пользователь файл
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                // Читаем текст из выбранного файла
                string inputText = System.IO.File.ReadAllText(openFileDialog.FileName);

                // Генерация случайного ключа для RC4
                byte[] key = GenerateRandomKeyForRC4();

                string keyString = Convert.ToBase64String(key);

                // Шифрование
                string encryptedText = EncryptRC4(inputText, key);
                label3.Text = "Зашифрованный текст (RC4): " + encryptedText;

                // Дешифрование
                string decryptedText = DecryptRC4(encryptedText, key);
                label7.Text = "Дешифрованный текст (RC4): " + decryptedText;

                // Вызов диалогового окна для выбора пути сохранения файла
                using (SaveFileDialog saveFileDialog = new SaveFileDialog())
                {
                    saveFileDialog.FileName = "RC4.txt"; // Имя файла по умолчанию
                    saveFileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
                    saveFileDialog.Title = "Сохранить зашифрованный текст";

                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        string filePath = saveFileDialog.FileName;

                        try
                        {
                            // Запись зашифрованного текста и ключа в выбранный файл
                            File.WriteAllText(filePath, $"Зашифрованный текст: {encryptedText}\nКлюч: {keyString}\nРасшифрованный текст: {decryptedText}");
                            MessageBox.Show($"Зашифрованный текст и ключ успешно сохранены в файл: {filePath}", "Сохранение успешно", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Ошибка при сохранении файла: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Сохранение отменено.", "Отмена", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }


            }
            else
            {
                MessageBox.Show("Файл не был выбран.");
            }

        }

        // Генерация случайного ключа для RC4 (длина ключа 128 бит / 16 байт)
        private byte[] GenerateRandomKeyForRC4()
        {
            Random random = new Random(); // Создаем объект Random для генерации случайных чисел
            byte[] key = new byte[16]; // Создаем массив байтов для ключа длиной 16 байт (128 бит)
            random.NextBytes(key); // Заполняем массив случайными байтами, что и является сгенерированным ключом
            return key; // Возвращаем сгенерированный случайный ключ
        }

        // Шифрование текста с использованием RC4
        private string EncryptRC4(string plainText, byte[] key)
        {
            // Преобразуем входной текст (plainText) в массив байтов с использованием UTF8-кодировки
            byte[] plainBytes = Encoding.UTF8.GetBytes(plainText);

            // Шифруем байты текста с использованием метода RC4
            byte[] encryptedBytes = RC4(plainBytes, key);

            // Преобразуем зашифрованный массив байтов в строку в формате Base64 и возвращаем результат
            return Convert.ToBase64String(encryptedBytes); // Возвращаем зашифрованный текст как строку Base64
        }

        // Дешифрование текста с использованием RC4
        private string DecryptRC4(string encryptedText, byte[] key)
        {
            // Преобразуем зашифрованную строку в формат Base64 в массив байтов
            byte[] encryptedBytes = Convert.FromBase64String(encryptedText);

            // Дешифруем зашифрованные байты с использованием метода RC4
            byte[] decryptedBytes = RC4(encryptedBytes, key);

            // Преобразуем дешифрованный массив байтов обратно в строку с использованием UTF8-кодировки
            return Encoding.UTF8.GetString(decryptedBytes); // Возвращаем дешифрованный текст
        }

        // Алгоритм RC4 (реализация самого шифра)
        private byte[] RC4(byte[] data, byte[] key)
        {
            int keyLength = key.Length; // Длина ключа (обычно 128 бит, то есть 16 байт)

            // Массив s (перестановочный массив), который будет использоваться для генерации потока ключа
            byte[] s = new byte[256]; // Инициализируем массив s, длина 256 
            byte[] t = new byte[256]; // Вспомогательный массив t, который содержит данные ключа
            byte[] output = new byte[data.Length]; // Массив для хранения результата шифрования или дешифрования

            // Инициализация массивов s и t
            for (int i = 0; i < 256; i++)
            {
                s[i] = (byte)i; // Заполняем массив s числами от 0 до 255
                t[i] = key[i % keyLength]; // Заполняем массив t данными из ключа. Если ключ короче 256, то повторяем его.
            }

            // Перемешивание массива s с использованием массива t
            int j = 0;
            for (int i = 0; i < 256; i++)
            {
                j = (j + s[i] + t[i]) % 256; // Обновляем индекс j
                byte temp = s[i]; // Меняем элементы массива s на основе значений из массива t
                s[i] = s[j]; // Меняем значения элементов массива s
                s[j] = temp; // Завершаем обмен
            }

            // Генерация ключевого потока и шифрование/дешифрование данных
            int i1 = 0, j1 = 0; // Индексы для перемешивания массива s
            for (int k = 0; k < data.Length; k++)
            {
                // Обновляем индексы i1 и j1
                i1 = (i1 + 1) % 256; // Индекс i1 меняется по кругу от 0 до 255
                j1 = (j1 + s[i1]) % 256; // Индекс j1 зависит от значений в массиве s

                byte temp = s[i1]; // Меняем элементы массива s
                s[i1] = s[j1]; // Меняем элементы массива s на основе индексов i1 и j1
                s[j1] = temp; // Завершаем обмен

                byte keyStreamByte = s[(s[i1] + s[j1]) % 256]; // Генерируем байт ключевого потока, используя элементы массива s
                output[k] = (byte)(data[k] ^ keyStreamByte); // Шифруем или дешифруем байт с помощью XOR с ключевым потоком
            }

            // Возвращаем результат шифрования/дешифрования
            return output;
        }
        /*
          Принцип работы алгоритма RC4:

          1. **Инициализация**:
             - RC4 - это потоковый симметричный шифр, который использует ключ переменной длины для создания ключевого потока.
             - Сначала создается два массива: 
               - Массив `s` (перестановочный массив) размером 256 байтов, который будет использоваться для генерации ключевого потока.
               - Массив `t`, который заполняется данными ключа.

          2. **Инициализация массивов**:
             - Массив `s` инициализируется числами от 0 до 255.
             - Массив `t` заполняется байтами ключа. Если длина ключа меньше 256 байтов, то ключ повторяется, пока не заполнятся все элементы массива `t`.

          3. **Перемешивание массива `s`**:
             - Используя массив `t`, происходит перемешивание элементов массива `s` методом обмена элементов с индексами, зависящими от текущих значений в массиве `s` и `t`.
             - Этот шаг делает ключевой поток (генерируемый в последующих шагах) случайным и непредсказуемым.

          4. **Генерация ключевого потока**:
             - Для каждого байта данных генерируется соответствующий байт ключевого потока, используя элементы массива `s`. Индексы для генерации ключевого потока обновляются после каждого шага.
             - Для шифрования или дешифрования данных используется операция побитового исключающего ИЛИ (XOR) между данным байтом и соответствующим байтом ключевого потока.

          5. **Шифрование/Дешифрование**:
             - Чтобы зашифровать или расшифровать данные, каждый байт исходных данных (или зашифрованных данных) комбинируется с байтом ключевого потока с помощью операции XOR.
             - Эта операция позволяет RC4 быть симметричным шифром: процесс шифрования и дешифрования одинаков, так как операция XOR является обратимой.

          6. **Реализация**:
             - В процессе работы алгоритм генерирует один байт ключевого потока за раз. Этот ключевой поток используется для шифрования или дешифрования данных.
             - Результатом работы алгоритма является либо зашифрованный текст, либо расшифрованный текст, в зависимости от того, какой поток данных подается на вход шифра.

          
           RC4 имеет некоторые уязвимости, связанные с его слабым начальным состоянием ключевого потока, и рекомендуется использовать более безопасные алгоритмы для защиты критически важной информации.
        */


        // Обработчик нажатия кнопки для шифрования и дешифрования
        private void button4_Click(object sender, EventArgs e)
        {
            string plainText = textBox1.Text; // Получаем текст из textBox1

            // Генерируем случайный ключ и IV для шифрования
            byte[] key = GenerateRandomKeyForDES();
            byte[] iv = GenerateRandomIV2();

            // Шифруем текст
            string encryptedText = EncryptDES(plainText, key, iv);
            label4.Text = "Зашифрованный текст(Variable DES): " + encryptedText; // Выводим зашифрованный текст в label4

            // Дешифруем текст
            string decryptedText = DecryptDES(encryptedText, key, iv);
            label8.Text = "Дешифрованный текст(Variable DES): " + decryptedText; // Выводим расшифрованный текст в label8

            string keyString = Convert.ToBase64String(key);
            string ivString = Convert.ToBase64String(iv);

            // Вызов диалогового окна для выбора пути сохранения файла
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.FileName = "Variable DES.txt"; // Имя файла по умолчанию
                saveFileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
                saveFileDialog.Title = "Сохранить зашифрованный текст";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string filePath = saveFileDialog.FileName;

                    try
                    {
                        // Запись зашифрованного текста и ключа в выбранный файл
                        File.WriteAllText(filePath, $"Зашифрованный текст: {encryptedText}\nКлюч:\n {keyString} \n {ivString}\nРасшифрованный текст: {decryptedText}");
                        MessageBox.Show($"Зашифрованный текст и ключ успешно сохранены в файл: {filePath}", "Сохранение успешно", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка при сохранении файла: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Сохранение отменено.", "Отмена", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }




        }

        private void button12_Click(object sender, EventArgs e)
        {
            // Создаем диалог для выбора файла
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";

            // Проверяем, выбрал ли пользователь файл
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                // Читаем текст из выбранного файла
                string plainText = System.IO.File.ReadAllText(openFileDialog.FileName);

                // Генерируем случайный ключ и IV для шифрования
                byte[] key = GenerateRandomKeyForDES();
                byte[] iv = GenerateRandomIV2();

                string keyString = Convert.ToBase64String(key);
                string ivString = Convert.ToBase64String(iv);

                // Шифруем текст
                string encryptedText = EncryptDES(plainText, key, iv);
                label4.Text = "Зашифрованный текст(Variable DES): " + encryptedText; // Выводим зашифрованный текст в label4

                // Дешифруем текст
                string decryptedText = DecryptDES(encryptedText, key, iv);
                label8.Text = "Дешифрованный текст(Variable DES): " + decryptedText; // Выводим расшифрованный текст в label8

                // Вызов диалогового окна для выбора пути сохранения файла
                using (SaveFileDialog saveFileDialog = new SaveFileDialog())
                {
                    saveFileDialog.FileName = "Variable DES.txt"; // Имя файла по умолчанию
                    saveFileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
                    saveFileDialog.Title = "Сохранить зашифрованный текст";

                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        string filePath = saveFileDialog.FileName;

                        try
                        {
                            // Запись зашифрованного текста и ключа в выбранный файл
                            File.WriteAllText(filePath, $"Зашифрованный текст: {encryptedText}\nКлюч:\n {keyString} \n {ivString}\nРасшифрованный текст: {decryptedText}");
                            MessageBox.Show($"Зашифрованный текст и ключ успешно сохранены в файл: {filePath}", "Сохранение успешно", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Ошибка при сохранении файла: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Сохранение отменено.", "Отмена", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }


            }
            else
            {
                MessageBox.Show("Файл не был выбран.");
            }

        }
        // Генерация случайного ключа для DES (длина ключа 56 бит)
        private byte[] GenerateRandomKeyForDES()
        {
            // Создаем объект алгоритма DES
            using (DES des = DES.Create())
            {
                // Генерация случайного ключа (по умолчанию 56 бит для DES)
                des.GenerateKey();

                // Возвращаем сгенерированный ключ
                return des.Key;
            }
        }

        // Генерация случайного IV (инициализирующего вектора) для DES
        private byte[] GenerateRandomIV2()
        {
            // Создаем объект алгоритма DES
            using (DES des = DES.Create())
            {
                // Генерация случайного инициализирующего вектора (IV)
                des.GenerateIV();

                // Возвращаем сгенерированный IV
                return des.IV;
            }
        }

        // Шифрование текста с использованием DES
        private string EncryptDES(string plainText, byte[] key, byte[] iv)
        {
            // Создаем объект алгоритма DES
            using (DES des = DES.Create())
            {
                // Устанавливаем ключ и IV для DES
                des.Key = key;
                des.IV = iv;

                // Создаем объект для шифрования с использованием заданного ключа и IV
                ICryptoTransform encryptor = des.CreateEncryptor(key, iv);

                // Используем MemoryStream для записи зашифрованных данных в память
                using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
                {
                    // Используем CryptoStream для шифрования данных, записываемых в MemoryStream
                    using (CryptoStream cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                    {
                        // Записываем строку (plainText) в CryptoStream, что приведет к ее шифрованию
                        using (System.IO.StreamWriter sw = new System.IO.StreamWriter(cs))
                        {
                            sw.Write(plainText);
                        }
                    }
                    // Возвращаем зашифрованный текст, преобразованный в строку Base64
                    return Convert.ToBase64String(ms.ToArray());
                }
            }
        }

        // Дешифрование текста с использованием DES
        private string DecryptDES(string encryptedText, byte[] key, byte[] iv)
        {
            // Создаем объект алгоритма DES
            using (DES des = DES.Create())
            {
                // Устанавливаем ключ и IV для DES
                des.Key = key;
                des.IV = iv;

                // Создаем объект для дешифрования с использованием заданного ключа и IV
                ICryptoTransform decryptor = des.CreateDecryptor(key, iv);

                // Используем MemoryStream для чтения зашифрованных данных (encryptedText) из памяти
                using (System.IO.MemoryStream ms = new System.IO.MemoryStream(Convert.FromBase64String(encryptedText)))
                {
                    // Используем CryptoStream для дешифрования данных, считанных из MemoryStream
                    using (CryptoStream cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                    {
                        // Используем StreamReader для чтения дешифрованных данных как строки
                        using (System.IO.StreamReader sr = new System.IO.StreamReader(cs))
                        {
                            // Возвращаем дешифрованный текст
                            return sr.ReadToEnd();
                        }
                    }
                }
            }
        }
        /*
   Принцип работы алгоритма DES (Data Encryption Standard):

   1. **Ключ и данные**:
      Алгоритм DES использует симметричное шифрование, где для шифрования и дешифрования используется один и тот же секретный ключ. Этот ключ имеет фиксированную длину 56 бит (7 байт).

   2. **Разбиение данных**:
      Для шифрования данных DES делит исходные данные (например, текст) на блоки по 64 бита. Если данные не делятся на 64 бита, они могут быть дополнены до нужного размера.

   3. **Инициализирующий вектор (IV)**:
      В дополнение к ключу, для обеспечения безопасности используется инициализирующий вектор (IV), который случайным образом генерируется для каждого сеанса шифрования. Это помогает предотвратить использование одинаковых данных с одинаковыми ключами, увеличивая безопасность.

   4. **Раундовые операции**:
      Алгоритм состоит из 16 раундов, каждый из которых выполняет серию операций над блоком данных. Каждый раунд включает в себя:
      - Разделение блока данных на две половины.
      - Применение множества операций (например, перестановки, замены и операции XOR) с использованием ключа для каждого раунда.
      - Каждая итерация раунда использует подмножество оригинального ключа, которое генерируется через процесс ключевого расширения.

   5. **Финальная перестановка**:
      После завершения всех 16 раундов результат из двух половин блока снова объединяется и проходит через финальную перестановку. Это гарантирует, что результат шифрования является непредсказуемым и безопасным.

   6. **Дешифрование**:
      Процесс дешифрования в DES практически идентичен шифрованию, но используется обратный порядок раундов и тот же ключ.

   7. **Безопасность**:
      Несмотря на то, что DES был широко использован в прошлом, он больше не считается безопасным для защиты современных данных, так как его 56-битный ключ подвержен атаке "перебора" (brute-force). Однако алгоритм продемонстрировал важные принципы симметричного шифрования и является основой для более современных алгоритмов, таких как AES.
        */
        // Функция получения случайного числа
        private int Rand()//Ф-я получения случайного числа
        {
            Random random = new Random();
            return random.Next();
        }
        int power(int a, int b, int n) // a^b mod n - возведение a в степень b по модулю n
        {
            int tmp = a;
            int sum = tmp;
            for (int i = 1; i < b; i++)
            {
                for (int j = 1; j < a; j++)
                {
                    sum += tmp; // Увеличиваем сумму на текущее значение tmp
                    if (sum >= n) // Если сумма превышает n, то выполняем операцию взятия остатка по модулю n
                    {
                        sum -= n;
                    }
                }
                tmp = sum;
            }
            return tmp;
        }
        int mul(int a, int b, int n) // a*b mod n - умножение a на b по модулю n
        {
            int sum = 0;
            for (int i = 0; i < b; i++)
            {
                sum += a;
                if (sum >= n)
                {
                    sum -= n;
                }
            }
            return sum;
        }

        // Функция шифрования сообщения
        void crypt(int p, int g, int x, string strIn)
        {
            label10.Text = ""; // Очищаем текст в label10 перед шифрованием
            int y = power(g, x, p); // Вычисляем публичный ключ y = g^x mod p
            label10.Text = "Открытый ключ (p,g,y) = (" + p + "," + g + "," + y + ")" + "\n Закрытый ключ x = " + x;

            IEnumerator<char> Enum = strIn.GetEnumerator(); // Получаем итератор для строки
            Enum.Reset();
            if (Enum.MoveNext()) // Если в строке есть символы, начинаем шифрование
            {
                char[] temp = new char[strIn.Length - 1];
                temp = strIn.ToCharArray();
                for (int i = 0; i <= strIn.Length - 1; i++)
                {
                    int m = (int)temp[i];
                    if (m > 0)
                    {
                        int k = Rand() % (p - 2) + 1; // 1 < k < (p-1)
                        int a = power(g, k, p);
                        int b = mul(power(y, k, p), m, p);
                        label10.Text = label10.Text + a + " " + b + " ";
                    }
                }
            }
        }
        void decrypt(int p, int x, string strIn)
        {
            if (strIn.Length > 0)
            {
                label9.Text = "";
                string[] strA = strIn.Split(' ');
                if (strA.Length > 0)
                {
                    for (int i = 0; i < strA.Length - 1; i += 2)
                    {
                        char[] a = new char[strA[i].Length];
                        char[] b = new char[strA[i + 1].Length];
                        int ai = 0;
                        int bi = 0;
                        a = strA[i].ToCharArray();
                        b = strA[i + 1].ToCharArray();
                        for (int j = 0; (j < a.Length); j++)
                        {
                            ai = ai * 10 + (int)(a[j] - 48);
                        }
                        for (int j = 0; (j < b.Length); j++)
                        {
                            bi = bi * 10 + (int)(b[j] - 48);
                        }
                        if ((ai != 0) && (bi != 0))
                        {
                            int deM = mul(bi, power(ai, p - 1 - x, p), p);// m=b*(a^x)^(-1)mod p =b*a^(p-1-x)mod p - трудно было  найти нормальную формулу, в ней вся загвоздка
                            char m = (char)deM;
                            label9.Text = label9.Text + m;
                        }
                    }
                }

            }
        }

        // Обработчик события нажатия кнопки button5 (начало шифрования и дешифрования)
        private void button5_Click(object sender, EventArgs e)
        {
            string strIn = textBox1.Text; // Получаем исходное сообщение из textBox1
            string strOut = label10.Text; // Считываем зашифрованное сообщение из label10

            int p = 23;  // Простое число (p) для алгоритма
            int g = 5;   // Примитивный корень (g) для алгоритма
            int x = Rand() % (p - 2) + 1;  // Генерируем случайное значение x (закрытый ключ)

            crypt(p, g, x, strIn); // Шифруем сообщение
            decrypt(p, x, strOut); // Дешифруем зашифрованное сообщение
        }

        private void button13_Click(object sender, EventArgs e)
        {
            // Создаем диалог для выбора файла
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";

            // Проверяем, выбрал ли пользователь файл
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                // Читаем текст из выбранного файла
                string strIn = System.IO.File.ReadAllText(openFileDialog.FileName);


                string strOut = label10.Text; // Считываем зашифрованное сообщение из label10

                int p = 23;  // Простое число (p) для алгоритма
                int g = 5;   // Примитивный корень (g) для алгоритма
                int x = Rand() % (p - 2) + 1;  // Генерируем случайное значение x (закрытый ключ)

                crypt(p, g, x, strIn); // Шифруем сообщение
                decrypt(p, x, strOut); // Дешифруем зашифрованное сообщение




            }
            else
            {
                MessageBox.Show("Файл не был выбран.");
            }
        }


        /*
Алгоритм Эль-Гамаля — это криптографическая схема с открытым ключом, основанная на сложности задачи дискретного логарифма. Он используется для шифрования и цифровых подписей.

1. Генерация ключей:
  - Выбирается большое простое число p.
  - Выбирается число g, которое является генератором группы чисел по модулю p.
  - Открытый ключ состоит из пары чисел (p, g, h), где h — это g^x mod p, а x — секретный ключ.
  - Секретный ключ — это число x, выбранное случайным образом в пределах от 1 до p-2.

2. Шифрование:
  - Для шифрования сообщения выбирается случайное число y (y ∈ [1, p-2]).
  - Вычисляется:
    * c1 = g^y mod p
    * c2 = m * h^y mod p, где m — это сообщение (представленное числом).
  - Зашифрованное сообщение состоит из пары чисел (c1, c2).

3. Расшифровка:
  - Для расшифровки зашифрованного сообщения (c1, c2) используется секретный ключ x.
  - Для восстановления исходного сообщения выполняются следующие шаги:
    * Вычисляется s = c1^x mod p
    * Находится обратный элемент для s по модулю p, т.е. s_inv = s^(p-2) mod p (используется малое теорема Ферма для нахождения обратного по модулю).
    * Исходное сообщение восстанавливается как m = (c2 * s_inv) mod p.

Примерный процесс шифрования и расшифровки можно представить в виде:

Генерация ключей:
  - p = 23, g = 5, x = 6 (секретный ключ)
  - h = g^x mod p = 5^6 mod 23 = 8 (открытый ключ)

Шифрование:
  - m = 15 (сообщение)
  - Выбираем случайное число y = 10
  - c1 = g^y mod p = 5^10 mod 23 = 9
  - c2 = m * h^y mod p = 15 * 8^10 mod 23 = 15 * 6 = 90 mod 23 = 21

  Зашифрованное сообщение: (c1, c2) = (9, 21)

Расшифровка:
  - Получаем зашифрованные данные: (c1 = 9, c2 = 21)
  - Вычисляем s = c1^x mod p = 9^6 mod 23 = 8
  - Находим обратный элемент: s_inv = 8^(23-2) mod 23 = 8^21 mod 23 = 3
  - Исходное сообщение m = (c2 * s_inv) mod p = (21 * 3) mod 23 = 63 mod 23 = 15

  Исходное сообщение восстановлено: m = 15.

Таким образом, с помощью алгоритма Эль-Гамаля мы можем шифровать и расшифровывать сообщения, используя публичный ключ для шифрования и приватный для расшифровки.
*/


        // Обработчик события кнопки button7
        private void button7_Click(object sender, EventArgs e)
        {
            // Берем текст из текстового поля textBox1
            string inputText = textBox1.Text;

            // Если текст пустой или состоит только из пробелов, выводим сообщение и прекращаем выполнение
            if (string.IsNullOrWhiteSpace(inputText))
            {
                label14.Text = "Введите текст для хэширования."; // Показываем пользователю сообщение об ошибке
                return; // Прерываем выполнение метода, если текст не введен
            }

            // Вычисляем хэш для введенного текста с помощью алгоритма FNV-1a
            uint hashValue = FNV1aHash(inputText);

            // Отображаем хэш в шестнадцатеричной форме в label14
            label14.Text = $"Хэш: {hashValue:X8}"; // Используем форматирование X8 для вывода 8-значного шестнадцатеричного хэша

            // Вызов диалогового окна для выбора пути сохранения файла
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                // Устанавливаем параметры диалога сохранения файла
                saveFileDialog.FileName = "FNV1aHash.txt"; // Имя файла по умолчанию
                saveFileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*"; // Фильтр для текстовых файлов
                saveFileDialog.Title = "Сохранить зашифрованный текст"; // Заголовок диалога

                // Проверяем, если пользователь выбрал файл и подтвердил сохранение
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string filePath = saveFileDialog.FileName; // Получаем путь к файлу

                    try
                    {
                        // Пытаемся записать хэш в выбранный файл
                        File.WriteAllText(filePath, $"Хэш текста: {hashValue}"); // Записываем хэш в файл
                                                                                 // Информируем пользователя об успешном сохранении
                        MessageBox.Show($"Зашифрованный текст и ключ успешно сохранены в файл: {filePath}",
                            "Сохранение успешно", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        // В случае ошибки при записи в файл выводим сообщение об ошибке
                        MessageBox.Show($"Ошибка при сохранении файла: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    // Если пользователь отменил сохранение, показываем сообщение об отмене
                    MessageBox.Show("Сохранение отменено.", "Отмена", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void button15_Click(object sender, EventArgs e)
        {
            // Создаем диалог для выбора файла
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";

            // Проверяем, выбрал ли пользователь файл
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                // Читаем текст из выбранного файла
                string inputText = System.IO.File.ReadAllText(openFileDialog.FileName);

                // Если текст пустой или состоит только из пробелов, выводим сообщение и прекращаем выполнение
                if (string.IsNullOrWhiteSpace(inputText))
                {
                    label14.Text = "Введите текст для хэширования."; // Показываем пользователю сообщение об ошибке
                    return; // Прерываем выполнение метода, если текст не введен
                }

                // Вычисляем хэш для введенного текста с помощью алгоритма FNV-1a
                uint hashValue = FNV1aHash(inputText);

                // Отображаем хэш в шестнадцатеричной форме в label14
                label14.Text = $"Хэш: {hashValue:X8}"; // Используем форматирование X8 для вывода 8-значного шестнадцатеричного хэша

                // Вызов диалогового окна для выбора пути сохранения файла
                using (SaveFileDialog saveFileDialog = new SaveFileDialog())
                {
                    // Устанавливаем параметры диалога сохранения файла
                    saveFileDialog.FileName = "FNV1aHash.txt"; // Имя файла по умолчанию
                    saveFileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*"; // Фильтр для текстовых файлов
                    saveFileDialog.Title = "Сохранить зашифрованный текст"; // Заголовок диалога

                    // Проверяем, если пользователь выбрал файл и подтвердил сохранение
                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        string filePath = saveFileDialog.FileName; // Получаем путь к файлу

                        try
                        {
                            // Пытаемся записать хэш в выбранный файл
                            File.WriteAllText(filePath, $"Хэш текста: {hashValue}\nТекст: {inputText}"); // Записываем хэш в файл
                                                                                     // Информируем пользователя об успешном сохранении
                            MessageBox.Show($"Зашифрованный текст и ключ успешно сохранены в файл: {filePath}",
                                "Сохранение успешно", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        catch (Exception ex)
                        {
                            // В случае ошибки при записи в файл выводим сообщение об ошибке
                            MessageBox.Show($"Ошибка при сохранении файла: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        // Если пользователь отменил сохранение, показываем сообщение об отмене
                        MessageBox.Show("Сохранение отменено.", "Отмена", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }

            }
            else
            {
                MessageBox.Show("Файл не был выбран.");
            }

        }

        // Алгоритм FNV-1a для вычисления хэша
        private uint FNV1aHash(string input)
        {
            uint fnvPrime = 0x01000193; // FNV prime
            uint offsetBasis = 0x811C9DC5; // FNV offset basis
            uint hash = offsetBasis; // Инициализируем хэш значением offsetBasis

            // Для каждого символа в строке выполняем операцию XOR и умножение на FNV prime
            foreach (char c in input)
            {
                hash ^= c; // XOR с символом
                hash *= fnvPrime; // Умножение на FNV prime
            }

            // Возвращаем вычисленный хэш
            return hash;
        }

        /*
            Принципы работы алгоритма FNV (Fowler–Noll–Vo):

            Алгоритм хэширования FNV используется для создания уникального хэша для строки или данных.
            Это простой, но эффективный способ хэширования, основанный на использовании операцией XOR и умножения на фиксированное простое число (FNV prime).

            Основные этапы работы алгоритма:

            1. **Инициализация**:
               - Алгоритм начинается с начального значения (offset basis), которое используется как исходная "основа" для хэширования.
               - Это значение отличается в разных версиях алгоритма FNV, например, для FNV-1a используется значение 0x811C9DC5.

            2. **Обработка каждого символа**:
               - Для каждого символа входной строки или данных алгоритм выполняет следующие операции:
                 1. Применяется операция **XOR** (исключающее ИЛИ) между текущим значением хэша и значением символа.
                 2. Полученный результат умножается на **фиксированное простое число** (FNV prime), которое равно 0x01000193 для FNV-1a.

            3. **Повторение процесса**:
               - Шаги XOR и умножения повторяются для каждого символа строки. Это приводит к созданию уникального хэш-значения для каждой строки.

            4. **Результат**:
               - После того как все символы строки обработаны, результат этих операций и будет хэшем строки.

            Алгоритм FNV-1a отличается от FNV-1 тем, что операция XOR выполняется **перед** умножением на FNV prime, что дает разные результаты при одинаковых данных. Этот маленький измененный шаг делает FNV-1a более устойчивым к коллизиям (случаям, когда два разных входных значения дают одинаковый хэш).

            Преимущества алгоритма:
            - Высокая скорость выполнения, так как использует простые операции (XOR и умножение).
            - Простой в реализации и хорошо подходит для задач, где требуется быстрое вычисление хэша для небольших строк данных.

            Недостатки:
            - Не является криптографически стойким и не предназначен для использования в безопасности, так как хэширование не имеет необходимых свойств (например, устойчивости к атакам).
        */

        // Обработчик события нажатия кнопки button8
        private void button8_Click(object sender, EventArgs e)
        {
            // Берем текст из текстового поля textBox1
            string inputText = textBox1.Text;

            // Проверка, если введенный текст пустой или состоит только из пробелов
            // В таком случае не будем выполнять хэширование и выведем сообщение об ошибке
            if (string.IsNullOrWhiteSpace(inputText))
            {
                label13.Text = "Введите текст для хэширования.";  // Выводим подсказку в метке label13
                return;  // Выход из метода, если текст пустой
            }

            // Если текст введен, вычисляем хэш с использованием алгоритма SHA-256
            string hashValue = ComputeSHA256Hash(inputText);

            // Отображаем хэш в метке label13
            label13.Text = $"SHA-256 хэш: {hashValue}";

            // Создаем диалоговое окно для сохранения файла
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                // Устанавливаем имя файла по умолчанию
                saveFileDialog.FileName = "SHA256Hash.txt";  // Имя файла по умолчанию для сохранения
                                                             // Устанавливаем фильтр для выбора файлов (только текстовые файлы и все файлы)
                saveFileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
                // Заголовок диалогового окна
                saveFileDialog.Title = "Сохранить зашифрованный текст";

                // Показываем диалоговое окно и проверяем, выбрал ли пользователь файл и подтвердил ли сохранение
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string filePath = saveFileDialog.FileName; // Получаем путь к файлу, выбранному пользователем

                    try
                    {
                        // Пытаемся записать хэш в выбранный файл
                        File.WriteAllText(filePath, $"Хэш текста: {hashValue}");  // Записываем хэш в файл

                        // Если запись прошла успешно, выводим сообщение об успешном сохранении
                        MessageBox.Show($"Зашифрованный текст и ключ успешно сохранены в файл: {filePath}",
                            "Сохранение успешно", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        // Если произошла ошибка при записи в файл, выводим сообщение об ошибке
                        MessageBox.Show($"Ошибка при сохранении файла: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    // Если пользователь отменил сохранение, выводим сообщение об отмене
                    MessageBox.Show("Сохранение отменено.", "Отмена", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void button16_Click(object sender, EventArgs e)
        {
            // Создаем диалог для выбора файла
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";

            // Проверяем, выбрал ли пользователь файл
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                // Читаем текст из выбранного файла
                string inputText = System.IO.File.ReadAllText(openFileDialog.FileName);

                // Проверка, если введенный текст пустой или состоит только из пробелов
                // В таком случае не будем выполнять хэширование и выведем сообщение об ошибке
                if (string.IsNullOrWhiteSpace(inputText))
                {
                    label13.Text = "Введите текст для хэширования.";  // Выводим подсказку в метке label13
                    return;  // Выход из метода, если текст пустой
                }

                // Если текст введен, вычисляем хэш с использованием алгоритма SHA-256
                string hashValue = ComputeSHA256Hash(inputText);

                // Отображаем хэш в метке label13
                label13.Text = $"SHA-256 хэш: {hashValue}";

                // Создаем диалоговое окно для сохранения файла
                using (SaveFileDialog saveFileDialog = new SaveFileDialog())
                {
                    // Устанавливаем имя файла по умолчанию
                    saveFileDialog.FileName = "SHA256Hash.txt";  // Имя файла по умолчанию для сохранения
                                                                 // Устанавливаем фильтр для выбора файлов (только текстовые файлы и все файлы)
                    saveFileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
                    // Заголовок диалогового окна
                    saveFileDialog.Title = "Сохранить зашифрованный текст";

                    // Показываем диалоговое окно и проверяем, выбрал ли пользователь файл и подтвердил ли сохранение
                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        string filePath = saveFileDialog.FileName; // Получаем путь к файлу, выбранному пользователем

                        try
                        {
                            // Пытаемся записать хэш в выбранный файл
                            File.WriteAllText(filePath, $"Хэш текста: {hashValue}\nТекст: {inputText}");  // Записываем хэш в файл

                            // Если запись прошла успешно, выводим сообщение об успешном сохранении
                            MessageBox.Show($"Зашифрованный текст и ключ успешно сохранены в файл: {filePath}",
                                "Сохранение успешно", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        catch (Exception ex)
                        {
                            // Если произошла ошибка при записи в файл, выводим сообщение об ошибке
                            MessageBox.Show($"Ошибка при сохранении файла: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        // Если пользователь отменил сохранение, выводим сообщение об отмене
                        MessageBox.Show("Сохранение отменено.", "Отмена", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }

            }
            else
            {
                MessageBox.Show("Файл не был выбран.");
            }

        }

        // Функция для вычисления хэша с использованием алгоритма SHA-256
        private string ComputeSHA256Hash(string input)
        {
            // Используем блок с конструкцией using для автоматического освобождения ресурсов
            using (SHA256 sha256Hash = SHA256.Create())  // Создаем экземпляр SHA-256 для вычисления хэша
            {
                // Преобразуем строку в массив байтов (используется кодировка UTF8)
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

                // Создаем StringBuilder для формирования строки хэша
                StringBuilder builder = new StringBuilder();

                // Перебираем каждый байт полученного хэша
                foreach (byte b in bytes)
                {
                    // Добавляем каждый байт как строку в шестнадцатеричном формате "x2"
                    builder.Append(b.ToString("x2"));
                }

                // Возвращаем сформированную строку хэша
                return builder.ToString();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }




        /*
Алгоритм хэширования SHA-256 (Secure Hash Algorithm 256-bit) является частью семейства алгоритмов SHA-2, предназначенных для создания хэш-кодов фиксированной длины (256 бит или 32 байта) на основе входных данных произвольной длины.

Основная цель хэширования — это получение уникального представления данных (хэша) для входного текста, которое будет иметь следующие характеристики:
1. **Фиксированная длина**: Независимо от длины входных данных (текста или файла), результат всегда будет иметь длину 256 бит (32 байта).
2. **Необратимость**: Хэш нельзя обратно преобразовать в исходные данные, т.е. процесс хэширования необратим.
3. **Уникальность**: Разные входные данные с очень высокой вероятностью будут иметь разные хэши. Даже малое изменение во входных данных приведет к совершенно другому хэшу (принцип "авторегулярности").
4. **Коллизии**: SHA-256 обеспечивает крайне низкую вероятность коллизий, т.е. ситуаций, когда два разных набора данных имеют одинаковые хэши.

Принцип работы алгоритма SHA-256 можно описать следующим образом:
1. Входные данные (например, текст) сначала преобразуются в последовательность бит (в кодировке UTF-8).
2. Затем данные обрабатываются в блоках по 512 бит, каждый из которых проходит серию операций с использованием ключевых параметров (например, сжатие данных через различные логические операции).
3. Алгоритм использует заранее определенные константы и операторы для комбинирования данных и их хэширования.
4. После обработки всех блоков данных результат сводится в финальный хэш — строку длиной 256 бит, которая представляет собой уникальное цифровое представление исходных данных.

SHA-256 используется в различных областях: для проверки целостности данных, создания цифровых подписей, а также в криптографических протоколах (например, в блокчейне).

В данном примере хэширование осуществляется с использованием стандартного механизма SHA-256, доступного в .NET через класс `SHA256`, который применяет алгоритм к тексту и возвращает его хэш в шестнадцатеричной строке.
*/

        private void button6_Click(object sender, EventArgs e)
        {

            // Получаем текст из textBox1
            string originalText = textBox1.Text;

            try
            {
                // Создаем RSA-пара ключей
                using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
                {
                    // Получаем открытый и закрытый ключи
                    RSAParameters publicKey = rsa.ExportParameters(false);
                    RSAParameters privateKey = rsa.ExportParameters(true);

                    // Шифруем сообщение
                    byte[] encryptedBytes = Encrypt(Encoding.UTF8.GetBytes(originalText), publicKey);
                    string encryptedText = Convert.ToBase64String(encryptedBytes);

                    // Преобразуем ключи в строку
                    string publicKeyString = ConvertPublicKeyToString(publicKey);
                    string privateKeyString = ConvertPrivateKeyToString(privateKey);


                    // Отображаем зашифрованное сообщение в label12
                    label12.Text = encryptedText;

                    // Расшифровываем сообщение
                    byte[] decryptedBytes = Decrypt(encryptedBytes, privateKey);
                    string decryptedText = Encoding.UTF8.GetString(decryptedBytes);

                    // Отображаем расшифрованное сообщение в label11
                    label11.Text = decryptedText + "\n" + "Ключи:" + "\n" + publicKeyString + "\n" + privateKeyString;
                

                // Вызов диалогового окна для выбора пути сохранения файла
                using (SaveFileDialog saveFileDialog = new SaveFileDialog())
                {
                    saveFileDialog.FileName = "RSA.txt"; // Имя файла по умолчанию
                    saveFileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
                    saveFileDialog.Title = "Сохранить зашифрованный текст";

                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        string filePath = saveFileDialog.FileName;

                        try
                        {
                            // Запись зашифрованного текста и ключа в выбранный файл
                            File.WriteAllText(filePath, $"Зашифрованный текст: {encryptedText}\nКлючи:\n {publicKeyString} + {privateKeyString}\nРасшифрованный текст: {decryptedText}");
                            MessageBox.Show($"Зашифрованный текст и ключ успешно сохранены в файл: {filePath}", "Сохранение успешно", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Ошибка при сохранении файла: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Сохранение отменено.", "Отмена", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка: " + ex.Message);
            }
        }

        private void button14_Click(object sender, EventArgs e)
        {

            // Создаем диалог для выбора файла
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";

            // Проверяем, выбрал ли пользователь файл
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                // Читаем текст из выбранного файла
                string inputText = System.IO.File.ReadAllText(openFileDialog.FileName);

                try
                {
                    // Создаем RSA-пара ключей
                    using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
                    {
                        // Получаем открытый и закрытый ключи
                        RSAParameters publicKey = rsa.ExportParameters(false);
                        RSAParameters privateKey = rsa.ExportParameters(true);

                        // Шифруем сообщение
                        byte[] encryptedBytes = Encrypt(Encoding.UTF8.GetBytes(inputText), publicKey);
                        string encryptedText = Convert.ToBase64String(encryptedBytes);

                        // Преобразуем ключи в строку
                        string publicKeyString = ConvertPublicKeyToString(publicKey);
                        string privateKeyString = ConvertPrivateKeyToString(privateKey);


                        // Отображаем зашифрованное сообщение в label12
                        label12.Text = encryptedText;

                        // Расшифровываем сообщение
                        byte[] decryptedBytes = Decrypt(encryptedBytes, privateKey);
                        string decryptedText = Encoding.UTF8.GetString(decryptedBytes);

                        // Отображаем расшифрованное сообщение в label11
                        label11.Text = decryptedText + "\n" + "Ключи:" + "\n" + publicKeyString + "\n" + privateKeyString;


                        // Вызов диалогового окна для выбора пути сохранения файла
                        using (SaveFileDialog saveFileDialog = new SaveFileDialog())
                        {
                            saveFileDialog.FileName = "RSA.txt"; // Имя файла по умолчанию
                            saveFileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
                            saveFileDialog.Title = "Сохранить зашифрованный текст";

                            if (saveFileDialog.ShowDialog() == DialogResult.OK)
                            {
                                string filePath = saveFileDialog.FileName;

                                try
                                {
                                    // Запись зашифрованного текста и ключа в выбранный файл
                                    File.WriteAllText(filePath, $"Зашифрованный текст: {encryptedText}\nКлючи:\n {publicKeyString} + {privateKeyString}\nРасшифрованный текст: {decryptedText}");
                                    MessageBox.Show($"Зашифрованный текст и ключ успешно сохранены в файл: {filePath}", "Сохранение успешно", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show($"Ошибка при сохранении файла: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                            else
                            {
                                MessageBox.Show("Сохранение отменено.", "Отмена", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка: " + ex.Message);
                }

            }
            else
            {
                MessageBox.Show("Файл не был выбран.");
            }
        }

        private byte[] Encrypt(byte[] data, RSAParameters publicKey)
        {
            using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
            {
                rsa.ImportParameters(publicKey);
                return rsa.Encrypt(data, false);
            }
        }

        private byte[] Decrypt(byte[] data, RSAParameters privateKey)
        {
            using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
            {
                rsa.ImportParameters(privateKey);
                return rsa.Decrypt(data, false);
            }
        }

        // Преобразование открытого ключа в строку
        private string ConvertPublicKeyToString(RSAParameters key)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Modulus: " + Convert.ToBase64String(key.Modulus));
            sb.AppendLine("Exponent: " + Convert.ToBase64String(key.Exponent));
            return sb.ToString();
        }

        // Преобразование закрытого ключа в строку
        private string ConvertPrivateKeyToString(RSAParameters key)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Modulus: " + Convert.ToBase64String(key.Modulus));
            sb.AppendLine("Exponent: " + Convert.ToBase64String(key.Exponent));
            sb.AppendLine("D: " + Convert.ToBase64String(key.D));
            sb.AppendLine("P: " + Convert.ToBase64String(key.P));
            sb.AppendLine("Q: " + Convert.ToBase64String(key.Q));
            sb.AppendLine("DP: " + Convert.ToBase64String(key.DP));
            sb.AppendLine("DQ: " + Convert.ToBase64String(key.DQ));
            sb.AppendLine("InverseQ: " + Convert.ToBase64String(key.InverseQ));
            return sb.ToString();
        }
        /*
        Алгоритм RSA (Rivest–Shamir–Adleman) — это асимметричный криптографический алгоритм, который используется для шифрования и цифровой подписи данных. Он основан на сложности факторизации больших чисел и работает с использованием пары ключей: открытого (public) и закрытого (private).

        Принцип работы алгоритма RSA:

        1. Генерация ключей:
           - Выбираются два больших простых числа p и q.
           - Вычисляется их произведение n = p × q, которое станет частью открытого и закрытого ключей. Число n должно быть достаточно большим, чтобы обеспечить безопасность (обычно от 2048 бит и выше).
           - Вычисляется значение функции Эйлера φ(n) = (p - 1) × (q - 1).
           - Выбирается целое число e, которое должно быть взаимно простым с φ(n) и больше 1. Обычно e выбирается равным 65537, так как это простое число, и оно обеспечивает эффективное шифрование.
           - Вычисляется значение d, такое что d × e ≡ 1 (mod φ(n)). Это значение можно найти с помощью расширенного алгоритма Евклида, и d будет частью закрытого ключа.
           - Таким образом, открытый ключ состоит из пары чисел (n, e), а закрытый ключ — из пары (n, d).

        2. Шифрование:
           - Отправитель берет открытый ключ получателя (n, e).
           - Сообщение (обычно оно представляется как число M) преобразуется в число, которое меньше n.
           - Сообщение шифруется с помощью формулы:
             C = M^e mod n
             где C — зашифрованный текст.

        3. Расшифровка:
           - Получатель, обладая закрытым ключом (n, d), расшифровывает сообщение, используя формулу:
             M = C^d mod n
             где M — исходное сообщение.

        Пример работы RSA:
           - Пусть p = 61 и q = 53, тогда n = 61 × 53 = 3233 и φ(n) = (61 - 1) × (53 - 1) = 3120.
           - Выбираем e = 17, взаимно простое с 3120. Находим d = 2753, так как 2753 × 17 ≡ 1 (mod 3120).
           - Открытый ключ — (3233, 17), закрытый — (3233, 2753).
           - Чтобы зашифровать сообщение M = 65, вычисляем C = 65^17 mod 3233 = 2790.
           - Для расшифровки сообщения C = 2790 применяем M = 2790^2753 mod 3233 = 65.

        Особенности RSA:
           - RSA базируется на сложности факторизации числа n на множители p и q. При больших значениях p и q факторизация становится практически невозможной, что обеспечивает высокий уровень безопасности.
           - RSA медленнее симметричных алгоритмов, таких как AES, поэтому его часто используют для шифрования небольших объемов данных, например, для передачи симметричных ключей.
        */











    }
}


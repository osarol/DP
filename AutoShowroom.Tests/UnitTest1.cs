using Xunit;
using AutoShowroomApp;

namespace AutoShowroom.Tests
{
    public class PasswordHelperTests
    {
        /// <summary>
        /// Тест 1: Перевіряє, що метод HashPassword генерує унікальний хеш, 
        /// і цей хеш не є порожнім рядком або null.
        /// </summary>
        [Fact]
        public void HashPassword_ShouldReturnValidCorrectString()
        {
            // Arrange
            string password = "ManagerSecurePassword2026";

            // Act
            string hash = PasswordHelper.HashPassword(password);

            // Assert
            Assert.NotNull(hash);
            Assert.NotEmpty(hash);
            string[] parts = hash.Split('.');
            Assert.Equal(3, parts.Length);
            Assert.Equal("100000", parts[0]); 
        }

        /// <summary>
        /// Тест 2: Позитивний сценарій. Перевіряє, що якщо ми передаємо 
        /// правильний пароль, метод VerifyPassword повертає true.
        /// </summary>
        [Fact]
        public void VerifyPassword_WithCorrectPassword_ShouldReturnTrue()
        {
            // Arrange
            string password = "SuperAdmin_Password";
            string generatedHash = PasswordHelper.HashPassword(password);

            // Act
            bool isValid = PasswordHelper.VerifyPassword(password, generatedHash);

            // Assert
            Assert.True(isValid, "Метод повинен підтвердити правильний пароль.");
        }

        /// <summary>
        /// Тест 3: Негативний сценарій. Перевіряє, що якщо користувач вводить 
        /// неправильний пароль, метод VerifyPassword повертає false.
        /// </summary>
        [Fact]
        public void VerifyPassword_WithWrongPassword_ShouldReturnFalse()
        {
            // Arrange
            string correctPassword = "CorrectPassword123";
            string wrongPassword = "WrongPassword123";
            string generatedHash = PasswordHelper.HashPassword(correctPassword);

            // Act
            bool isValid = PasswordHelper.VerifyPassword(wrongPassword, generatedHash);

            // Assert
            Assert.False(isValid, "Метод повинен відхилити некоректний пароль.");
        }

        /// <summary>
        /// Тест 4: Параметризований тест (Theory). Дозволяє запустити один і той самий 
        /// алгоритм перевірки для великої кількості різних варіацій вхідних даних.
        /// </summary>
        [Theory]
        [InlineData("1")]                   // Дуже короткий пароль
        [InlineData("Password_With_Spaces ")] // Пароль із пробілом
        [InlineData("ЙЦУКЕН_кирилиця_123")]  // Пароль національним алфавітом
        [InlineData("!@#$%^&*()_+{}|:<>?")] // Спеціальні символи
        public void VerifyPassword_ShouldWorkForVariousPasswordFormats(string diversePassword)
        {
            // Arrange & Act
            string hash = PasswordHelper.HashPassword(diversePassword);
            bool result = PasswordHelper.VerifyPassword(diversePassword, hash);

            // Assert
            Assert.True(result, $"Помилка обробки пароля у форматі: {diversePassword}");
        }

        /// <summary>
        /// Тест 5: Тестування стійкості до пошкоджених даних.
        /// Перевіряє, що якщо формат хешу в БД з якихось причин порушено 
        /// (немає крапок), додаток не впаде з критичною помилкою (Crash), 
        /// а метод безпечно поверне false завдяки блоку try-catch.
        /// </summary>
        [Fact]
        public void VerifyPassword_WithCorruptedHashFormat_ShouldReturnFalseSafe()
        {
            // Arrange
            string password = "AnyPassword";
            string corruptedHash = "InvalidHashWithoutDots"; // Немає розділювачів '.'

            // Act
            bool result = PasswordHelper.VerifyPassword(password, corruptedHash);

            // Assert
            Assert.False(result); 
        }
    }
}
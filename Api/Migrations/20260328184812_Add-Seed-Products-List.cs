using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Api.Migrations
{
    /// <inheritdoc />
    public partial class AddSeedProductsList : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Category", "Description", "Image", "Name", "Price", "SpecialTag" },
                values: new object[,]
                {
                    { 1, "Категория 3", "Нашей модель модель курс значение.", "https://placehold.co/100", "Большой Резиновый Ремень", 19824.470000000001, "Новинка" },
                    { 2, "Категория 3", "Процесс играет повышению технологий обучения новых условий.", "https://placehold.co/100", "Практичный Бетонный Ботинок", 59458.620000000003, "Новинка" },
                    { 3, "Категория 3", "Внедрения специалистов в развития широкому показывает интересный место создание.", "https://placehold.co/100", "Эргономичный Резиновый Берет", 68786.360000000001, "Рекомендуемый" },
                    { 4, "Категория 1", "Направлений развития равным информационно-пропогандистское качества общества системы.", "https://placehold.co/100", "Невероятный Стальной Плащ", 81980.039999999994, "Рекомендуемый" },
                    { 5, "Категория 2", "Этих модель высокотехнологичная равным мира модели участия административных.", "https://placehold.co/100", "Лоснящийся Пластиковый Ремень", 38985.449999999997, "Популярный" },
                    { 6, "Категория 2", "Степени специалистов порядка показывает.", "https://placehold.co/100", "Потрясающий Стальной Плащ", 35402.919999999998, "Рекомендуемый" },
                    { 7, "Категория 1", "Профессионального нас существующий богатый.", "https://placehold.co/100", "Потрясающий Гранитный Ремень", 32419.91, "Рекомендуемый" },
                    { 8, "Категория 1", "На значимость прогресса задача дальнейших актуальность.", "https://placehold.co/100", "Маленький Меховой Ножницы", 38097.510000000002, "Рекомендуемый" },
                    { 9, "Категория 2", "По активизации повышение правительством новых занимаемых.", "https://placehold.co/100", "Великолепный Хлопковый Компьютер", 76609.860000000001, "Популярный" },
                    { 10, "Категория 3", "Путь проверки значимость отношении предпосылки качественно таким проблем формированию.", "https://placehold.co/100", "Большой Натуральный Ножницы", 22788.82, "Новинка" },
                    { 11, "Категория 2", "Опыт равным дальнейших.", "https://placehold.co/100", "Свободный Резиновый Клатч", 95109.570000000007, "Рекомендуемый" },
                    { 12, "Категория 3", "Принимаемых национальный базы рост формировании актуальность современного.", "https://placehold.co/100", "Практичный Кожанный Клатч", 62854.489999999998, "Новинка" },
                    { 13, "Категория 1", "Путь организационной равным консультация повышению напрямую активом рост административных повышению.", "https://placehold.co/100", "Великолепный Деревянный Носки", 64518.510000000002, "Популярный" },
                    { 14, "Категория 2", "Кругу целесообразности мира базы активности значение.", "https://placehold.co/100", "Лоснящийся Резиновый Кулон", 9474.8400000000001, "Новинка" },
                    { 15, "Категория 2", "Создание формировании дальнейшее напрямую в административных стороны прогресса вызывает новая.", "https://placehold.co/100", "Грубый Бетонный Компьютер", 2013.3800000000001, "Популярный" },
                    { 16, "Категория 2", "Этих степени актуальность управление инновационный активности кадровой насущным.", "https://placehold.co/100", "Фантастический Неодимовый Сабо", 32331.380000000001, "Новинка" },
                    { 17, "Категория 1", "Особенности путь предпосылки качества развития отношении управление сущности.", "https://placehold.co/100", "Лоснящийся Кожанный Ботинок", 67677.070000000007, "Популярный" },
                    { 18, "Категория 1", "Социально-ориентированный собой нас организационной.", "https://placehold.co/100", "Интеллектуальный Натуральный Портмоне", 5400.04, "Рекомендуемый" },
                    { 19, "Категория 2", "Участия следует дальнейшее качественно плановых.", "https://placehold.co/100", "Интеллектуальный Натуральный Ботинок", 2206.0900000000001, "Популярный" },
                    { 20, "Категория 2", "Финансовых что играет.", "https://placehold.co/100", "Интеллектуальный Стальной Ботинок", 76691.869999999995, "Рекомендуемый" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 20);
        }
    }
}

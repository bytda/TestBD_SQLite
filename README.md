# TestBD_SQLite

Тестовое задание для C# разработчика

Требуется разработать приложение с графическим интерфейсом, которое подключается к базе данных и отображет для пользователя, данные из базы.

Требования к приложению
1. Gui приложения должен быть выполнен с использованием технологии Windows Presentation Foundation (WPF).
2. Приложение должно содержать gui-элемент,позволяющий пользователю выбрать БД для подключения. Gui-элемент для табличного представления данных, полученных из БД. До подключения к БД элемент табличного представления данных содержит только строку с названиями столбцов. Gui- элемент, взаимодействуя с которым, пользователь сохраняет содержимое элемента табличного представления данных в файл .csv.
3. После выбора пользовтелем БД для подключения приложение запрашивает необходимые данные из базы и наполняет ими элемент табличного представления данных.
4. Gui-элемент табличного представления данных должен содержать столбцы со следующими названиями: RunName, ItemTag, NPD, RunLength, LineWeight, RunDiam, PressureRating, FluidCode, Temp.
5. Данные для наполнения столбцов берутся из столбцов с теми же именами в БД.
6. Для каждого RunName нужно получать данные из БД, соответствующие этому имени.

База данных
1. Файл базы данных SQLite предоставляется разработчику вместе с этим заданием.
2. Организацию хранения данных в БД разработчику предлагается изучить самостоятельно.

Задания «со звёздочкой»
1. Изменение данных в ячейке элемента табличного представления данных приводит к изменению соответствующих данных в БД.
2. Удаление строки в элементе табличного представления данных приводит к удалению соответствующих данных из БД.

Оформление задания
1. Выполненное задание это zip-архив каталога с проектом VisualStudio (без каталогов bin и obj), содержащий все необходимые для корректной сборки, тестирования и запуска файлы исходных кодов, дополнительные
библиотеки/пакеты NuGet и другие ресурсы и зависимости, а так же каталог
.git.
2. Распакованный из архива проект должен собираться и запускаться без
необходимости ручной установки остутствующих в системе зависимостей.


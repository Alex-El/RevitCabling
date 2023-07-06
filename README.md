# RevitCabling

## Описание.

Плагин - хелпер для работы с инженерными коммуникациями.

*Решает проблему связи лотков и кабелей через общий параметр Cabling, добавляемый к лоткам. В этот параметр автоматически записываются номера электрических цепей.*

Процесс связи разбит на две процедуры:
- Оценка загруженности лотков.
При выполнении показываются текстовые аннотации для каждого лотка в которых описан процент заполнения лотка.
![](https://user-images.githubusercontent.com/83776033/251412666-6beb8498-dddd-4be1-a4c8-c2f2e36c8ca3.PNG)

- Графическое привязка кабелей к лоткам. 
Процедура позволяет юзеру интерактивно редактировать траекторию электрической цепи, одновременно привязывая цепь к лоткам через общий параметр.
![](https://user-images.githubusercontent.com/83776033/251412719-ae0abfd9-c39a-4a81-81ab-00b40fe41076.PNG)

## Установка.

Совместимость:
Revit 2022

Скачать msi, запустить.

Кнопка запуска плагина Cabling -> Put cable into trays:
![](https://user-images.githubusercontent.com/83776033/251412503-25be1c98-1298-44a9-98a6-be9e9b722d49.PNG)

Интерфейс (dockable - не блокирующее выполнение окно, позволяет прикрепление к интерфейсу Revit):
![](https://user-images.githubusercontent.com/83776033/251412594-745039bc-bde4-4278-bbfd-96b810ed10c7.PNG)

## Использование.

- Использовать на 3д виде. Дисциплина - Электросети. Локи прозрачность 50%.
- Проект должен иметь файл общих параметров.
- При старте плагина будет добавлен общий параметр Cabling

1. Загруженность лотков.
- Нажать кнопку “Загруженность лотков”
Оценить загруженность по текстовым меткам.
- При нажатии “Очистить” или “Ок” текстовые метки исчезнут.
2. Прокладка кабеля.
- Нажать кнопку “Прокладка кабеля”
Выбрать электрический потребитель. Отобразится текущая траектория электрической цепи.
- Выбрать лоток. Траектория цепи изменится, пройдя через лоток.
- При нажатии “Очистить” траектория исчезнет. Реальная трасса не изменится.
- При нажатии “Ок” траектория исчезнет. Реальная трасса будет соответствовать измененной траектории.


## Лицензия.

Apache license 2.0

## Контакты.

eltsov.online@gmail.com
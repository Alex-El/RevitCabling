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
Revit 2022, 2023, 2024

Скачать msi, запустить (см последний релиз).
https://github.com/Alex-El/RevitCabling/releases/download/0.0.1/RevitCablingSetup_22_23_24.msi

Кнопка запуска плагина Cabling -> Put cable into trays:
![](https://user-images.githubusercontent.com/83776033/251412503-25be1c98-1298-44a9-98a6-be9e9b722d49.PNG)

Интерфейс (dockable - не блокирующее выполнение окно, позволяет прикрепление к интерфейсу Revit):
![](https://user-images.githubusercontent.com/83776033/251412594-745039bc-bde4-4278-bbfd-96b810ed10c7.PNG)

## Использование.

- Использовать на 3д виде. Дисциплина - Электросети. Лотки прозрачность 50%.
- Проект должен иметь файл общих параметров.
- При старте плагина будет добавлен общий параметр Cabling

1. Загруженность лотков (не реализовано).
- Нажать кнопку “Загруженность лотков”
Оценить загруженность по текстовым меткам.
- При нажатии “Очистить” или “Ок” текстовые метки исчезнут.
2. Прокладка кабеля.
- Нажать кнопку “Прокладка кабеля”
Выбрать электрический потребитель. Отобразится текущая траектория электрической цепи.
- Выбрать лоток, начиная от щита. Траектория цепи изменится, пройдя через лоток.
- При нажатии “Очистить” траектория исчезнет. Реальная трасса не изменится.
- При нажатии “Ок” траектория исчезнет. Реальная трасса будет соответствовать измененной траектории.

## Лицензия.

Apache license 2.0

## Контакты.

eltsov.online@gmail.com

## ВАЖНО:

При построении трассы происходит проверка правильности геометрии трассы. Ревит имеет следующие допущения
```
The list of the electrical system circuit path node position is not valid. The length of the list should be more than one, the first node should be the position of the panel where the circuit begins at, the adjacent nodes should not be too close, and should be in the same level or on the same vertical line, to keep each segment of the circuit path always horizontal or vertical. Also note that the first node position should be the position of the connector (the one connects to the circuit) of the panel, but not the origin of the panel instance.
```
Кнопка OK не будет активна пока эти условя не выполнены.

## TodoHelper
### About
An application with basic todo management features, including custom categories, important flags, and optional due dates
### Built with
.NET 9, C#13
### Features / rules
+ Create a new category, requires a valid category name
+ Rename a category, requires a valid category name
+ Delete a category, if not related to any todos
+ Create a todo, requires a valid description and an optional due date, which can be in the past
+ Update a todo's description, if todo is not complete, requires a valid description
+ Update a todo's due date, if todo is not complete, due date is optional and can be in the past
+ Toggle todo is important/is not important, if todo is not complete
+ Toggle todo is complete/is not complete
+ Delete a todo, if it is not important
+ Move a todo to a different category
+ See all categories
+ See all todos for the selected category, sorted by due date descending, not including completed todos
+ See all todos due today, sorted by description, not including completed todos
+ See all todos that are overdue, sorted by due date descending, not including completed todos
+ See all todos that are important, sorted by due date descending, not including completed todos
+ See all completed todos sorted by due date descending
### Improvement opportunities
TBD
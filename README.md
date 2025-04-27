## TodoHelper
### About
An application with basic todo management features, including custom categories, important flags, and optional due dates
### Built with
.NET 9, C#13
### Features
+ See all categories
+ Create a new category
+ Create a todo
+ Update a todo's category
+ See all todos for the selected category, sorted by due date descending, not including completed todos
+ See all todos that are important, sorted by due date descending, not including completed todos
+ See all todos due today, sorted by description, not including completed todos
+ See all todos that are overdue, sorted by due date descending, not including completed todos
+ See all completed todos sorted by due date descending
+ Update a todo's description
+ Update a todo's due date
+ Toggle todo is important/is not important
+ Toggle todo is complete/is not complete
+ Rename a category
+ Delete a todo
+ Delete a category
### Rules
+ Categories having todo 'children' cannot be deleted; first delete the category's todos
+ Category name must be forty (40) characters or fewer
+ Category name must be unique
+ Category name must not be null, an empty string, nor all-whitespace characters
+ Complete todos cannot be updated, except to toggle to not complete
+ Important todos cannot be deleted
+ Todo description must be 255 characters or fewer
+ Todo description must not be null, an empty string, nor all-whitespace characters
+ Todos must have a category
### Improvement opportunities
TBD

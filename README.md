## TodoHelper
### About
An application with basic todo management features, including custom categories, important flags, and optional due dates
### Built with
.NET 9, C#13
### Features
+ See categories, sorted by name
+ Create a category
+ Create a todo
+ Update a todo's category
+ See all todos for a category, sorted by is complete descending, then by due date descending, then by description
+ See all todos that are important, not including completed todos, sorted by due date descending, then by description
+ See all todos due today, not including completed todos, sorted by description
+ See all todos that are overdue, not including completed todos, sorted by due date descending, then by description
+ See all completed todos sorted by due date descending, then by description
+ Update a todo's description
+ Update a todo's due date
+ Update a todo's importance
+ Update a todo's complete date
+ Update a category’s name
+ Delete a todo
+ Delete a category
### Rules
+ Category name must be forty (40) characters or fewer
+ Category name must be unique
+ Category name must not be null, an empty string, nor all-whitespace characters
+ Complete todos cannot be updated, except to update to not complete
+ Important todos cannot be deleted
+ Todo description must be 255 characters or fewer
+ Todo description must not be null, an empty string, nor all-whitespace characters
+ Todos must have a category
### Improvement opportunities
TBD

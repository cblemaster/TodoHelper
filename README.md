## TodoHelper
### About
An application with basic todo management features, including custom categories, important flags, and optional due dates
### Built with
+ .NET 9, C#13
+ ASP.NET Core
+ EF Core 9.x.x
### Features
|#|Feature|Specifications|Rules|
|-|-|-|-|
|1|Create category|n/a|Category name must be forty (40) characters or fewer|
|1|Create category|n/a|Category name must be unique|
|1|Create category|n/a|Category name must not be null, an empty string, nor all-whitespace characters|
|2|Create todo|n/a|Todo description must be 255 characters or fewer|
|2|Create todo|n/a|Todo description must not be null, an empty string, nor all-whitespace characters|
|2|Create todo|n/a|Todos must have a category|
|3|Delete category|n/a|n/a|
|4|Delete todo|n/a|Important todos cannot be deleted|
|5|Get categories|Order by name|n/a|
|6|Get category|n/a|n/a|
|7|Get todo|n/a|n/a|
|8|Get todos|Order by due date descending then by description|n/a|
|9|Get todos by category|Order by due date descending then by description; filter category; filter include/exclude complete|n/a|
|10|Get completed todos|Order by due date descending then by description; filter complete date|n/a|
|11|Get todos due today|Order by description; filter due date; filter include/exclude complete|n/a|
|12|Get important todos|Order by due date descending then by description; filter importance; filter include/exclude complete|n/a|
|13|Get overdue todos|Order by due date descending then by description; filter due date; filter include/exclude complete|n/a|
|14|Update category|n/a|Category name must be forty (40) characters or fewer|
|14|Update category|n/a|Category name must be unique|
|14|Update category|n/a|Category name must not be null, an empty string, nor all-whitespace characters|
|15|Update todo|n/a|Complete todos cannot be updated, except to update to not complete|
|15|Update todo|n/a|Todo description must be 255 characters or fewer|
|15|Update todo|n/a|Todo description must not be null, an empty string, nor all-whitespace characters|
|15|Update todo|n/a|Todos must have a category|
|16|Update todo complete date|n/a|n/a|
### Improvement opportunities
+ Tackle TODO comments
+ Add automated testing (unit tests, endpoint tests, what else?)
+ Add logging (errors, what else?)
+ Override db context SaveChanges() to set create and update dates in data access project
+ Condider base classes for commands/queries and responses in application project
+ Reduce handler code duplication and complexity in application project
+ Add both high-level and detailed architecture diagrams
+ Standardize crud operation names, esp. concerning gets, in data access project
+ Replace conditionals with pattern matching where possible
+ Add new errors as needed by the application project
+ Add features: recurring todos, sub-todos, reminders

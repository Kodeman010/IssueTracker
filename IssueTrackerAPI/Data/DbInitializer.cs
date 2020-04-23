﻿using IssueTrackerAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IssueTrackerAPI.Data
{
    public static class DbInitializer
    {
        public static void Initialize(ApplicationDbContext context)
        {
            context.Database.EnsureCreated();

            if (context.People.Any())
            {
                return;
            }



            var projects = new Project[]
            {
                new Project{Title = "Project #1", Description = "Very long description", CreationDate = DateTime.Parse("2007-09-01")},
                new Project{Title = "Project #2", Description = "long description", CreationDate = DateTime.Parse("2012-11-05")},
                new Project{Title = "Project #3", Description = "description", CreationDate = DateTime.Parse("2015-06-23")}
            };

            foreach (Project p in projects)
            {
                context.Projects.Add(p);
            }
            context.SaveChanges();



            var people = new Person[]
            {
                new Person{FirstName = "Jan", LastName = "Kowalski", Email = "test@test.com"},
                new Person{FirstName = "Emil", LastName = "Nowak", Email = "test2@test.com"}
            };

            foreach (Person p in people)
            {
                context.People.Add(p);
            }
            context.SaveChanges();



            var severities = new Severity[]
            {
                new Severity{SeverityName = "Minor"},
                new Severity{SeverityName = "Major"}
            };

            foreach (Severity s in severities)
            {
                context.Severities.Add(s);
            }
            context.SaveChanges();


            var statuses = new Status[]
            {
                new Status{StatusName = "Open"},
                new Status{StatusName = "In progress"},
                new Status{StatusName = "Closed"}
            };

            foreach (Status s in statuses)
            {
                context.Statuses.Add(s);
            }
            context.SaveChanges();




            var issues = new Issue[]
            {
                new Issue{Title = "Problem #1", Description = "Very very long description",
                        AuthorId = people.Single( i => i.LastName == "Nowak").PersonId,
                        ProjectId = projects.Single( i => i.Title == "Project #1").ProjectId,
                        SeverityId = severities.Single( i => i.SeverityName == "Minor").SeverityId,
                        StatusId = statuses.Single( i => i.StatusName == "Open").StatusId }
            };

            foreach (Issue i in issues)
            {
                context.Issues.Add(i);
            }
            context.SaveChanges();




            var projectMembers = new ProjectMember[]
            {
                new ProjectMember{
                    ProjectId = projects.Single( i => i.Title == "Project #1").ProjectId,
                    PersonId = people.Single( i => i.LastName == "Nowak").PersonId },

                new ProjectMember{
                    ProjectId = projects.Single( i => i.Title == "Project #1").ProjectId,
                    PersonId = people.Single( i => i.LastName == "Kowalski").PersonId }
            };

            foreach (ProjectMember p in projectMembers)
            {
                context.ProjectMembers.Add(p);
            }
            context.SaveChanges();





            var assignees = new Assignee[]
            {
                new Assignee{
                    IssueId = issues.Single( i => i.Title == "Problem #1").IssueId,
                    PersonId = people.Single( i => i.LastName == "Kowalski").PersonId }
            };

            foreach (Assignee a in assignees)
            {
                context.Assignees.Add(a);
            }
            context.SaveChanges();





            var comments = new Comment[]
            {
                new Comment{ Content = "Good job", CreationDate = DateTime.Parse("2016-06-23"),
                        IssueId = issues.Single( i => i.Title == "Problem #1").IssueId,
                        PersonId = people.Single( i => i.LastName == "Nowak").PersonId }
            };

            foreach (Comment c in comments)
            {
                context.Comments.Add(c);
            }
            context.SaveChanges();
        }
    }
}
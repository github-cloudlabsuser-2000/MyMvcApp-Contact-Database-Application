using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MyMvcApp.Models;

namespace MyMvcApp.Controllers;

public class UserController : Controller
{
    public static System.Collections.Generic.List<User> userlist = new System.Collections.Generic.List<User>();

    // GET: User
    public ActionResult Index()
    {
        // Pass the userlist to the view
        return View(userlist);
    }

    // GET: User/Details/5
    public ActionResult Details(int id)
    {
        // Find the user with the specified ID
        var user = userlist.FirstOrDefault(u => u.Id == id);

        // If the user is not found, return a NotFound result
        if (user == null)
        {
            return NotFound();
        }

        // Pass the user to the Details view
        return View(user);
    }

    // GET: User/Create
    public ActionResult Create()
    {
        // Return the Create view to allow the user to input data for a new user
        return View();
    }

    // POST: User/Create
    [HttpPost]
    public ActionResult Create(User user)
    {
        if (ModelState.IsValid)
        {
            // Add the new user to the userlist
            userlist.Add(user);

            // Redirect to the Index action
            return RedirectToAction(nameof(Index));
        }

        // If the model state is invalid, return the Create view with the current user data
        return View(user);
    }

    // GET: User/Edit/5
    public ActionResult Edit(int id)
    {
        // Retrieve the user with the specified ID
        var user = userlist.FirstOrDefault(u => u.Id == id);

        // If the user is not found, return a NotFound result
        if (user == null)
        {
        return NotFound();
        }

        // Pass the user to the Edit view
        return View(user);
    }

    // POST: User/Edit/5
    [HttpPost]
    public ActionResult Edit(int id, User user)
    {
        if (ModelState.IsValid)
        {
        // Find the existing user in the list
        var existingUser = userlist.FirstOrDefault(u => u.Id == id);

        // If the user is not found, return a NotFound result
        if (existingUser == null)
        {
            return NotFound();
        }

        // Update the user's properties
        existingUser.Name = user.Name;
        existingUser.Email = user.Email;

        // Redirect to the Index action
        return RedirectToAction(nameof(Index));
        }

        // If the model state is invalid, return the Edit view with the current user data
        return View(user);
    }

    // GET: User/Delete/5
    public ActionResult Delete(int id)
    {
        // Retrieve the user with the specified ID
        var user = userlist.FirstOrDefault(u => u.Id == id);

        // If the user is not found, return a NotFound result
        if (user == null)
        {
            return NotFound();
        }

        // Pass the user to the Delete view
        return View(user);
    }

    // POST: User/Delete/5
    [HttpPost]
    public ActionResult Delete(int id, IFormCollection collection)
    {
        // Find the user in the list
        var user = userlist.FirstOrDefault(u => u.Id == id);

        // If the user is not found, return a NotFound result
        if (user == null)
        {
            return NotFound();
        }

        // Remove the user from the list
        userlist.Remove(user);

        // Redirect to the Index action
        return RedirectToAction(nameof(Index));
    }

    public ActionResult Search(string? name)
    {
        // Check if the userlist is empty
        if (!userlist.Any())
        {
            // Return a view with a message indicating that the list is empty
            ViewBag.Message = "The user list is empty.";
            return View("Index", new List<User>());
        }

        // If name is null or empty, return the entire user list
        if (string.IsNullOrWhiteSpace(name))
        {
            ViewBag.Message = "Showing all users.";
            return View("Index", userlist);
        }

        // Filter the user list by partial name match
        var filteredUsers = userlist
            .Where(u => u.Name.IndexOf(name, StringComparison.OrdinalIgnoreCase) >= 0)
            .ToList();

        // Check if no users match the search criteria
        if (!filteredUsers.Any())
        {
            // Return a view with a message indicating no users were found
            ViewBag.Message = $"No users found containing the name '{name}'.";
            return View("Index", new List<User>());
        }

        // Pass the filtered users to the Index view
        return View("Index", filteredUsers);
    }
}

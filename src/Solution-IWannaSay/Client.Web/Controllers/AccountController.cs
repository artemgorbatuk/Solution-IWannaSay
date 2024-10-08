﻿using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Services.Models;
using System.Security.Claims;
using Services.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace Client.Web.Controllers;

[Authorize]
public class AccountController : Controller {

    private readonly IServiceAuthentification serviceAuthentification;
    private readonly IServiceRoom serviceRoom;
    private readonly ILogger<AccountController> logger;

    public AccountController(IServiceAuthentification serviceAuthentification, ILogger<AccountController> logger, IServiceRoom serviceRoom) {
        this.serviceAuthentification = serviceAuthentification;
        this.logger = logger;
        this.serviceRoom = serviceRoom;
    }

    [HttpGet]
    [AllowAnonymous]
    public IActionResult Login() {
        return View();
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> Login(LoginPage model) {

        if (!ModelState.IsValid) {
            ModelState.AddModelError(string.Empty, "НЕ заполнен логин и(или) пароль");
            return View(model);
        }

        var isUserExist = await serviceAuthentification.IsUserExist(model.Login, model.Password);

        if (!isUserExist) {
            ModelState.AddModelError(string.Empty, "Некорректные логин и(или) пароль");
            return View(model);
        }

        var identity = serviceAuthentification.GetIdentity(model.Login);

        var principal = new ClaimsPrincipal(identity);

        var properties = new AuthenticationProperties { IsPersistent = true };

        await HttpContext.SignInAsync("Cookies", principal, properties);

        logger.LogInformation("Login to the app has been successful");

        await serviceRoom.ConnectAsync();

        logger.LogInformation("Connect to the signalr service has been successful");

        return RedirectToAction("Index", "Home");
    }

    public async Task<IActionResult> Logout() {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

        await serviceRoom.DisconnectAsync();

        return RedirectToAction("Login", "Account");
    }
}
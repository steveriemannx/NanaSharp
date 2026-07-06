using Nana;

// ── Hello World — Demonstrates the core NanaSharp API ────────────────

var form = new Form("Hello NanaSharp!", 400, 300);

var label = new Label(form, "Welcome to NanaSharp!", 10, 10, 380, 30);
label.CenterText();

var nameLabel = new Label(form, "Your name:", 10, 50, 100, 24);

var nameBox = new TextBox(form, 110, 48, 260, 28);

var greetBtn = new Button(form, "Greet", 10, 90, 100, 30);

var outputLabel = new Label(form, "", 10, 135, 380, 80);

var quitBtn = new Button(form, "Quit", 310, 90, 100, 30);

greetBtn.Click += (_, _) =>
{
    var name = nameBox.Text.Trim();
    if (string.IsNullOrEmpty(name))
        outputLabel.Caption = "Please enter your name.";
    else
        outputLabel.Caption = $"Hello, {name}! Welcome to Nana on C#!";
};

quitBtn.Click += (_, _) =>
{
    form.Close();
    Application.Exit();
};

form.Show();
Console.WriteLine("NanaSharp HelloWorld is running. Close the window to exit.");
Application.Run();
Console.WriteLine("Goodbye!");

// See https://aka.ms/new-console-template for more information
//Console.WriteLine("Hello, World!");


/*
The SOLID Design Principles are used to manage most of the Software Design Problems we, as developers, generally encounter in our day-to-day programming. 
These design principles are tested and proven mechanisms to make software designs more understandable, flexible, and maintainable. 
As a result, if we follow these principles while designing our applications, we can develop better applications.

SOLID Design Principles represent five Design Principles used to make software designs more understandable, flexible, and maintainable. The Five SOLID Design Principles are as follows:

S -----> stands for the Single Responsibility Principle, also known as SRP. This Principle states that each software module or class should have only one reason to change. 
         In other words, each module or class should have only one responsibility.

O -----> stands for the Open-Closed Principle, also known as OSP. The Open-Closed Principle states that software entities, 
         such as modules, classes, functions, etc., should be open for extension but closed for modification.

L -----> stands for the Liskov Substitution Principle, also known as LSP. This Principle states that the object of a derived class should be able
         to replace an object of the base class without causing any errors in the system or modifying the behavior of the base class. 
         That means the child class objects should be able to replace parent class objects without changing the correctness or behavior of the program.

I -----> stand for the Interface Segregation Principle, also known as ISP. This Principle states that Clients should not be forced to implement any methods they don’t use
         Rather than one fat interface, numerous little interfaces are preferred based on groups of methods, with each interface serving one submodule.

D -----> stands for Dependency Inversion Principle, also known as DIP. This Principle states that high-level modules/classes should not depend on low-level modules/classes. 
         Both should depend upon abstractions. Secondly, abstractions should not depend upon details. Details should depend upon abstractions.

Applying the SOLID principles in our Application Development can make our application code more modular, easier to understand, and less error-prone when changes are made. 
Applying these principles enables us to create software that is simple to maintain and expand and can adapt better to future changes.
(https://dotnettutorials.net/course/solid-design-principles/)
 */

// --------------------------------- Single Responsibility Principle (SRP)  --------------------------------- //

// Violation Example:


using System;

public class Invoice // Here, the Invoice class handles calculation, printing, and database operations, violating SRP
{
    public void CalculateTotal() { /*...*/}
    public void PrintInvoice() { /*...*/}
    public void SaveToDatabase() { /*...*/}
}

// Adherence Example:

public class InvoiceCalculate
{
    public void CalculateTotal() { /*...*/ }
}

public class InvoicePrinter
{
    public void Print(Invoice invoice) { /*...*/ }
}

public class InvoiceRepository
{
    public void Save(Invoice invoice) { /*...*/ }
}

//Each class now has a single responsibility: Invoice for calculations, InvoicePrinter for printing, and InvoiceRepository for database operations.

// --------------------------------- Open/Closed Principle (OCP)  --------------------------------- //

// Violation Example:

public class Circle
{
    public double Radius { get; set; }
}
public class Square
{
    public double Side { get; set; }
}

public class AreaCalculator
{
    public double CalculateArea(object shape)
    {
        if (shape is Circle)
        {
            Circle c = (Circle)shape;
            return Math.PI * c.Radius * c.Radius;
        }
        else if (shape is Square)
        {
            Square s = (Square)shape;
            return s.Side * s.Side;
        }
        // Adding a new shape requires modifying this method
        return 0;
    }
}
// Adding a new shape necessitates modifying the CalculateArea method, violating OCP.

// Adherence Example:

public interface IShape
{
    double CalculateArea();
}
public class Circle2 : IShape
{
    public double Radius { get; set; }
    public double CalculateArea() => Math.PI * Radius * Radius;
}
public class Square2 : IShape
{
    public double Side { get; set; }
    public double CalculateArea() => Side * Side;
}
public class AreaCalculator2
{
    public double CalculateArea(IShape shape) => shape.CalculateArea();
}
// Now, new shapes can be added without modifying existing code, adhering to OCP.

// --------------------------------- Liskov Substitution Principle (LSP)  --------------------------------- //

//Violation Example:

public class Bird
{
    public virtual void Fly() { /*...*/ }
}

public class Ostrich : Bird
{
    public override void Fly()
    {
        throw new NotImplementedException();
    }
}
// An Ostrich cannot fly, so substituting Bird with Ostrich violates LSP


// Adherence Example:

public abstract class Bird2 { }

public interface IFlyingBird
{
    void Fly();
}

public class Sparrow : Bird2, IFlyingBird
{
    public void Fly() { /*...*/ }
}

public class Ostrich2 : Bird2 { /*...*/ }

// Now, only birds that can fly implement IFlyingBird, adhering to LSP.

// --------------------------------- Interface Segregation Principle (ISP)  --------------------------------- //

// Violation Example:

public interface IMultiFunctionDevice
{
    void Print();
    void Scan();
    void Fax();
}

public class BasicPrinter : IMultiFunctionDevice
{
    public void Print() { /*...*/ }
    public void Scan() { throw new NotImplementedException(); }
    public void Fax() { throw new NotImplementedException(); }
}
// BasicPrinter is forced to implement methods it doesn't need, violating ISP.

// Adherence Example:

public interface IPrinter
{
    void Print();
}

public interface IScanner
{
    void Scan();
}

public class BasicPrinter2 : IPrinter
{
    public void Print() { /*...*/ }
}

// Interfaces are now segregated based on functionality, adhering to ISP.


// --------------------------------- Dependency Inversion Principle (DIP)  --------------------------------- //

// Violation Example:

public class DataAccess
{
    public void GetData() { /*...*/ }
}

public class BusinessLogic
{
    private DataAccess dataAccess = new DataAccess();
    public void ProcessData()
    {
        dataAccess.GetData();
        // Process data
    }
}

// BusinessLogic depends directly on DataAccess, violating DIP.

// Adherence Example:

public interface IDataAccess
{
    void GetData();
}

public class DataAccess2 : IDataAccess
{
    public void GetData() { /*...*/ }
}

public class BusinessLogic2
{
    private readonly IDataAccess dataAccess;
    public BusinessLogic2(IDataAccess dataAccess)
    {
        this.dataAccess = dataAccess;
    }
    public void ProcessData()
    {
        dataAccess.GetData();
        // Process data
    }
}

// BusinessLogic depends on the abstraction IDataAccess, not the concrete DataAccess, adhering to DIP.
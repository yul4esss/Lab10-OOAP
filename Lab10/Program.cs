// Interface to declare visitor
public interface IVisitor
{
    void VisitComponent(Paragraph paragraph);
}

// Document interface with Accept method to accept visitors
public interface IDocumentElement
{
    void Accept(IVisitor visitor);
}

// Class that represents paragraphs
public class Paragraph : IDocumentElement
{
    public string Text { get; set; }
    public string TextStyle { get; set; }

    // User's text
    public Paragraph(string text)
    {
        Text = text;
    }

    // Passing Visitor object as a parameter to allow user to process the text
    public void Accept(IVisitor visitor)
    {
        visitor.VisitComponent(this);
    }
}


public class ParagraphStyleVisitor : IVisitor
{
    private string textStyle;

    public ParagraphStyleVisitor(string style)
    {
        textStyle = style;
    }

    public void VisitComponent(Paragraph paragraph)
    {
        paragraph.TextStyle = textStyle;
    }
}

public class ParagraphStyleInterface
{
    public static string GetParagraphStyle(int paragraphNumber)
    {
        while (true)
        {
            Console.WriteLine($"Select style for paragraph {paragraphNumber}:");
            Console.WriteLine("1. Inline");
            Console.WriteLine("2. Column");
            var choice = Console.ReadLine();

            if (choice == "1")
            {
                return "Inline";
            }
            else if (choice == "2")
            {
                return "Column";
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter 1 for Inline or 2 for Column.");
            }
        }
    }
}

namespace Lab10
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var document = new List<IDocumentElement>
            {
                new Paragraph("First paragraph"),
                new Paragraph("Second paragraph")
            };

            int paragraphNumber = 1;

            foreach (var element in document)
            {
                var style = ParagraphStyleInterface.GetParagraphStyle(paragraphNumber++);
                var visitor = new ParagraphStyleVisitor(style);
                element.Accept(visitor);
            }

            foreach (var element in document)
            {
                if (element is Paragraph paragraph)
                {
                    Console.WriteLine($"{paragraph.Text}, Style: {paragraph.TextStyle}");
                }
            }
        }
    }
}

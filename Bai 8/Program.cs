using System;

namespace bai8
{
    // 1. Lớp trừu tượng gốc Shape
    public abstract class Shape
    {
        public abstract void draw();
        public abstract void erase();
        public abstract void move(int x, int y);
    }

    // 2. Các lớp con kế thừa trực tiếp từ Shape
    public class Circle : Shape
    {
        public override void draw()
        {
            Console.WriteLine("Đang vẽ hình tròn (Circle)...");
        }

        public override void erase()
        {
            Console.WriteLine("Đang xóa hình tròn (Circle)...");
        }

        public override void move(int x, int y)
        {
            Console.WriteLine($"Di chuyển hình tròn đến tọa độ ({x}, {y})");
        }
    }

    public class Triangle : Shape
    {
        public override void draw()
        {
            Console.WriteLine("Đang vẽ hình tam giác (Triangle)...");
        }

        public override void erase()
        {
            Console.WriteLine("Đang xóa hình tam giác (Triangle)...");
        }

        public override void move(int x, int y)
        {
            Console.WriteLine($"Di chuyển hình tam giác đến tọa độ ({x}, {y})");
        }
    }

    public class Polygon : Shape
    {
        public override void draw()
        {
            Console.WriteLine("Đang vẽ đa giác (Polygon)...");
        }

        public override void erase()
        {
            Console.WriteLine("Đang xóa đa giác (Polygon)...");
        }

        public override void move(int x, int y)
        {
            Console.WriteLine($"Di chuyển đa giác đến tọa độ ({x}, {y})");
        }
    }

    public class Quad : Shape
    {
        public override void draw()
        {
            Console.WriteLine("Đang vẽ tứ giác (Quad)...");
        }

        public override void erase()
        {
            Console.WriteLine("Đang xóa tứ giác (Quad)...");
        }

        public override void move(int x, int y)
        {
            Console.WriteLine($"Di chuyển tứ giác đến tọa độ ({x}, {y})");
        }
    }

    // 3. Lớp Rectangle kế thừa nhiều tầng (từ Quad)
    public class Rectangle : Quad
    {
        public override void draw()
        {
            Console.WriteLine("Đang vẽ hình chữ nhật (Rectangle)...");
        }

        public override void erase()
        {
            Console.WriteLine("Đang xóa hình chữ nhật (Rectangle)...");
        }

        public override void move(int x, int y)
        {
            Console.WriteLine($"Di chuyển hình chữ nhật đến tọa độ ({x}, {y})");
        }
    }

    // 4. Lớp chính chứa hàm Main để chạy chương trình
    public class Draw
    {
        // Phương thức nhận tham số đa hình
        public void drawShape(Shape theShape)
        {
            theShape.draw();
        }

        public static void Main(string[] args)
        {
            // Hỗ trợ in tiếng Việt ra màn hình Console
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            Draw drawing = new Draw();

            // Khởi tạo các đối tượng lớp con thông qua kiểu của lớp cha
            Shape circle = new Circle();
            Shape quad = new Quad();
            Shape rectangle = new Rectangle();
            Shape triangle = new Triangle();
            Shape polygon = new Polygon();

            Console.WriteLine("--- THỰC THI TÍNH ĐA HÌNH VỚI DRAWSHAPE ---");
            drawing.drawShape(circle);
            drawing.drawShape(quad);
            drawing.drawShape(rectangle);
            drawing.drawShape(triangle);
            drawing.drawShape(polygon);

            Console.WriteLine("\n--- THỬ NGHIỆM CÁC PHƯƠNG THỨC KHÁC ---");
            circle.move(15, 30);
            rectangle.erase();

            // Dừng màn hình để xem kết quả
            Console.ReadLine();
        }
    }
}
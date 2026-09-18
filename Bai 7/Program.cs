using System;
using System.Collections.Generic;
using System.Linq;

namespace QuanLyDoiTuong
{
    // ==========================================
    // 1. LỚP CƠ SỞ TRỪU TƯỢNG: Person
    // ==========================================
    public abstract class Person
    {
        public string HoTen { get; set; }
        public string DiaChi { get; set; }

        public Person(string hoTen, string diaChi)
        {
            HoTen = string.IsNullOrWhiteSpace(hoTen) ? "Chưa cập nhật" : hoTen;
            DiaChi = string.IsNullOrWhiteSpace(diaChi) ? "Chưa cập nhật" : diaChi;
        }

        // Phương thức trừu tượng: Các lớp con bắt buộc phải ghi đè
        public abstract override string ToString();
    }

    // ==========================================
    // 2. LỚP SINH VIÊN: Student
    // ==========================================
    public class Student : Person
    {
        public double DiemToan { get; set; }
        public double DiemLy { get; set; }
        public double DiemHoa { get; set; }

        public Student(string hoTen, string diaChi, double diemToan, double diemLy, double diemHoa) 
            : base(hoTen, diaChi)
        {
            DiemToan = diemToan;
            DiemLy = diemLy;
            DiemHoa = diemHoa;
        }

        public double TinhDiemTrungBinh()
        {
            return (DiemToan + DiemLy + DiemHoa) / 3.0;
        }

        public string DanhGia()
        {
            double dtb = TinhDiemTrungBinh();
            if (dtb >= 8.0) return "Giỏi";
            if (dtb >= 6.5) return "Khá";
            if (dtb >= 5.0) return "Trung bình";
            return "Yếu";
        }

        public override string ToString()
        {
            return $"[Sinh Viên] {HoTen,-15} | Đ/c: {DiaChi,-10} | ĐTB: {TinhDiemTrungBinh():0.0} ({DanhGia()})";
        }
    }

    // ==========================================
    // 3. LỚP NHÂN VIÊN: Employee
    // ==========================================
    public class Employee : Person
    {
        public decimal HeSoLuong { get; set; }
        public decimal LuongCoBan { get; set; }

        public Employee(string hoTen, string diaChi, decimal heSoLuong, decimal luongCoBan) 
            : base(hoTen, diaChi)
        {
            HeSoLuong = heSoLuong;
            LuongCoBan = luongCoBan;
        }

        public decimal TinhLuong()
        {
            return HeSoLuong * LuongCoBan;
        }

        public override string ToString()
        {
            return $"[Nhân Viên] {HoTen,-15} | Đ/c: {DiaChi,-10} | Lương: {TinhLuong():N0} VNĐ";
        }
    }

    // ==========================================
    // 4. LỚP KHÁCH HÀNG: Customer
    // ==========================================
    public class Customer : Person
    {
        public string TenCongTy { get; set; }
        public decimal TriGiaHoaDon { get; set; }

        public Customer(string hoTen, string diaChi, string tenCongTy, decimal triGiaHoaDon) 
            : base(hoTen, diaChi)
        {
            TenCongTy = tenCongTy;
            TriGiaHoaDon = triGiaHoaDon;
        }

        public override string ToString()
        {
            string danhGia = TriGiaHoaDon >= 10000000m ? "Khách VIP" : "Khách Thường";
            return $"[Khách Hàng] {HoTen,-15} | Đ/c: {DiaChi,-10} | Cty: {TenCongTy,-10} | Hóa đơn: {TriGiaHoaDon:N0} ({danhGia})";
        }
    }

    // ==========================================
    // 5. LỚP QUẢN LÝ: Management
    // ==========================================
    public class Management
    {
        private List<Person> danhSach;

        public Management()
        {
            danhSach = new List<Person>();
        }

        public void ThemNguoi(Person p)
        {
            danhSach.Add(p);
        }

        public void SapXepTheoTen()
        {
            // Sắp xếp danh sách theo tên (Alphabet)
            danhSach = danhSach.OrderBy(p => p.HoTen).ToList();
            Console.WriteLine("\n=> Đã sắp xếp danh sách theo Tên (A-Z)!");
        }

        public void HienThiDanhSach()
        {
            Console.WriteLine(new string('=', 90));
            Console.WriteLine("DANH SÁCH QUẢN LÝ (SINH VIÊN, NHÂN VIÊN, KHÁCH HÀNG)");
            Console.WriteLine(new string('=', 90));

            if (danhSach.Count == 0)
            {
                Console.WriteLine("Danh sách trống!");
                return;
            }

            foreach (var p in danhSach)
            {
                Console.WriteLine(p.ToString());
            }
            Console.WriteLine(new string('-', 90));
        }
    }

    // ==========================================
    // 6. CHƯƠNG TRÌNH CHÍNH
    // ==========================================
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            Management ql = new Management();

            // 1. Thêm một vài Sinh viên
            ql.ThemNguoi(new Student("Trần Anh Khoa", "Hà Nội", 8.5, 7.0, 9.0));
            ql.ThemNguoi(new Student("Lê Bình", "Đà Nẵng", 4.0, 5.0, 5.5));

            // 2. Thêm một vài Nhân viên
            ql.ThemNguoi(new Employee("Nguyễn Văn Lợi", "TP.HCM", 2.5m, 5000000m));
            ql.ThemNguoi(new Employee("Phạm Thị Mai", "Cần Thơ", 3.0m, 5000000m));

            // 3. Thêm một vài Khách hàng
            ql.ThemNguoi(new Customer("Vương Trí", "Bình Dương", "FPT", 15000000m));
            ql.ThemNguoi(new Customer("Đinh Bão", "Đồng Nai", "VNPT", 5000000m));

            // Hiển thị danh sách ban đầu
            Console.WriteLine("--- DANH SÁCH BAN ĐẦU ---");
            ql.HienThiDanhSach();

            // Sắp xếp và hiển thị lại
            ql.SapXepTheoTen();
            ql.HienThiDanhSach();

            Console.WriteLine("\nBấm phím bất kỳ để thoát...");
            Console.ReadKey();
        }
    }
}
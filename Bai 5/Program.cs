using System;
using System.Collections.Generic;

namespace QuanLyNhanVien
{
    // 1. LỚP CƠ SỞ TRỪU TƯỢNG: Employee (Đã sửa lỗi chính tả từ Enployee)
    public abstract class Employee
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string SSN { get; set; } // Số an sinh xã hội hoặc Mã nhân viên

        public Employee(string firstName, string lastName, string ssn)
        {
            FirstName = firstName;
            LastName = lastName;
            SSN = ssn;
        }

        public override string ToString()
        {
            return $"{FirstName} {LastName} | Mã NV: {SSN}";
        }

        // Phương thức trừu tượng tính lương (các lớp con bắt buộc phải ghi đè)
        public abstract decimal Earnings();
    }

    // 2. NHÂN VIÊN LƯƠNG CỐ ĐỊNH: SalariedEmployee
    public class SalariedEmployee : Employee
    {
        private decimal weeklySalary;

        public decimal WeeklySalary
        {
            get { return weeklySalary; }
            set { weeklySalary = value >= 0 ? value : 0; } // Đảm bảo lương không âm
        }

        public SalariedEmployee(string firstName, string lastName, string ssn, decimal salary)
            : base(firstName, lastName, ssn)
        {
            WeeklySalary = salary;
        }

        public override decimal Earnings()
        {
            return WeeklySalary;
        }

        public override string ToString()
        {
            return $"[Nhân viên lương cứng] {base.ToString()}\n  + Lương hàng tuần: {WeeklySalary:N0} VNĐ";
        }
    }

    // 3. NHÂN VIÊN LÀM THEO GIỜ: HourlyEmployee
    public class HourlyEmployee : Employee
    {
        private decimal wage;
        private decimal hours;

        public decimal Wage
        {
            get { return wage; }
            set { wage = value >= 0 ? value : 0; }
        }

        public decimal Hours
        {
            get { return hours; }
            set { hours = (value >= 0 && value <= 168) ? value : 0; } // Tối đa 168 giờ/tuần
        }

        public HourlyEmployee(string firstName, string lastName, string ssn, decimal hourlyWage, decimal hoursWorked)
            : base(firstName, lastName, ssn)
        {
            Wage = hourlyWage;
            Hours = hoursWorked;
        }

        public override decimal Earnings()
        {
            if (Hours <= 40)
                return Wage * Hours;
            else
                return (40 * Wage) + ((Hours - 40) * Wage * 1.5m); // Giờ làm thêm (Overtime) nhân hệ số 1.5
        }

        public override string ToString()
        {
            return $"[Nhân viên theo giờ] {base.ToString()}\n  + Mức lương/giờ: {Wage:N0} | Số giờ làm: {Hours}";
        }
    }

    // 4. NHÂN VIÊN NHẬN HOA HỒNG DỰA TRÊN DOANH THU: CommissionEmployee
    public class CommissionEmployee : Employee
    {
        private decimal grossSales;
        private decimal commissionRate;

        public decimal GrossSales
        {
            get { return grossSales; }
            set { grossSales = value >= 0 ? value : 0; }
        }

        public decimal CommissionRate
        {
            get { return commissionRate; }
            set { commissionRate = (value > 0 && value < 1) ? value : 0; }
        }

        public CommissionEmployee(string firstName, string lastName, string ssn, decimal sales, decimal rate)
            : base(firstName, lastName, ssn)
        {
            GrossSales = sales;
            CommissionRate = rate;
        }

        public override decimal Earnings()
        {
            return CommissionRate * GrossSales;
        }

        public override string ToString()
        {
            return $"[Nhân viên hoa hồng] {base.ToString()}\n  + Doanh thu: {GrossSales:N0} VNĐ | Tỷ lệ hoa hồng: {CommissionRate * 100}%";
        }
    }

    // 5. NHÂN VIÊN HOA HỒNG CÓ LƯƠNG CƠ BẢN: BasePlusCommissionEmployee
    public class BasePlusCommissionEmployee : CommissionEmployee
    {
        private decimal baseSalary;

        public decimal BaseSalary
        {
            get { return baseSalary; }
            set { baseSalary = value >= 0 ? value : 0; }
        }

        public BasePlusCommissionEmployee(string firstName, string lastName, string ssn, decimal sales, decimal rate, decimal salary)
            : base(firstName, lastName, ssn, sales, rate)
        {
            BaseSalary = salary;
        }

        public override decimal Earnings()
        {
            return BaseSalary + base.Earnings(); // Lương cơ bản + Hoa hồng
        }

        public override string ToString()
        {
            // Tái sử dụng ToString của lớp cha (CommissionEmployee) và thêm Lương cơ bản
            return $"[NV hoa hồng có lương cứng] {FirstName} {LastName} | Mã NV: {SSN}\n  + Lương cơ bản: {BaseSalary:N0} VNĐ | Doanh thu: {GrossSales:N0} VNĐ | Tỷ lệ hoa hồng: {CommissionRate * 100}%";
        }
    }

    // 6. CHƯƠNG TRÌNH CHÍNH
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            // Khởi tạo các đối tượng nhân viên khác nhau
            SalariedEmployee nv1 = new SalariedEmployee("Nguyễn", "Văn A", "NV001", 8000000m);

            // Làm 45 giờ: 40 giờ bình thường + 5 giờ tính OT x1.5
            HourlyEmployee nv2 = new HourlyEmployee("Trần", "Thị B", "NV002", 50000m, 45m);

            CommissionEmployee nv3 = new CommissionEmployee("Lê", "Văn C", "NV003", 100000000m, 0.05m); // Bán được 100tr, hoa hồng 5%

            BasePlusCommissionEmployee nv4 = new BasePlusCommissionEmployee("Phạm", "Thị D", "NV004", 50000000m, 0.04m, 4000000m);

            // Gộp tất cả vào danh sách đa hình (Polymorphism)
            List<Employee> danhSachNV = new List<Employee>() { nv1, nv2, nv3, nv4 };

            Console.WriteLine("================ DANH SÁCH NHÂN VIÊN VÀ LƯƠNG ================\n");

            foreach (var emp in danhSachNV)
            {
                Console.WriteLine(emp.ToString());

                // Yêu cầu đặc biệt của bài tập này: Nếu là nhân viên BasePlusCommissionEmployee, tăng 10% lương cứng
                if (emp is BasePlusCommissionEmployee baseEmp)
                {
                    Console.WriteLine("  -> [Thưởng] Tăng 10% lương cơ bản cho nhân viên này!");
                    decimal luongCu = baseEmp.BaseSalary;
                    baseEmp.BaseSalary *= 1.10m;
                    Console.WriteLine($"  -> Lương cơ bản mới: {baseEmp.BaseSalary:N0} VNĐ (Cũ: {luongCu:N0} VNĐ)");
                }

                Console.WriteLine($"=> TỔNG THU NHẬP (Earnings): {emp.Earnings():N0} VNĐ\n");
                Console.WriteLine(new string('-', 70) + "\n");
            }

            Console.WriteLine("Bấm phím bất kỳ để thoát...");
            Console.ReadKey();
        }
    }
}
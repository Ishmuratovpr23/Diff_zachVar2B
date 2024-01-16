using System.ComponentModel;

namespace WinFormsApp2
{
    public partial class Form1 : Form
    {


        private BindingList<Product> products = new BindingList<Product>();
        private BindingList<Group> groups = new BindingList<Group>();


        public Form1()
        {
            InitializeComponent();

            button1.Click += button1_Click;

            
            FillLists();

            listBox1.DataSource = products;
            listBox2.DataSource = groups;

            
            listBox1.DisplayMember = "ToString";
            listBox2.DisplayMember = "ToString";
        }

        private void FillLists()
        {
           
            products.Add(new Product { Id = 1, Name = "Печенье" });
            products.Add(new Product { Id = 2, Name = "Хлеб" });
            groups.Add(new Group { InventoryNumber = 1, ProductId = 1, Title = "Печенье", Price = 100 });
            groups.Add(new Group { InventoryNumber = 2, ProductId = 2, Title = "Хлеб", Price = 200 });
        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        public void RemoveGroupAndProducts(int groupId)
        {
            
            var groupToRemove = groups.FirstOrDefault(g => g.ProductId == groupId);

          
            if (groupToRemove == null)
                return;

            products.RemoveAll(p => p.Id == groupToRemove.ProductId);

        
            groups.Remove(groupToRemove);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (listBox2.SelectedIndex != -1)
            {
               
                var selectedGroup = (Group)listBox2.Items[listBox2.SelectedIndex];

                
                RemoveGroupAndProducts(selectedGroup.ProductId);
            }
        }
    }

    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public override string ToString()
        {
            return $"ID: {Id}, Name: {Name}";
        }
    }

    public class Group
    {
        public int InventoryNumber { get; set; }
        public int ProductId { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }

        public override string ToString()
        {
            return $"Inventory Number: {InventoryNumber}, Product ID: {ProductId}, Title: {Title}, Price: {Price}";
        }
    }

    public static class BindingListExtensions
    {
        public static void RemoveAll<T>(this BindingList<T> list, Func<T, bool> predicate)
        {
            var toRemove = list.Where(predicate).ToList();
            foreach (var item in toRemove)
            {
                list.Remove(item);
            }
        }
    }
}
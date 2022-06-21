using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

using TreeViewFiltering.Model;

namespace TreeViewFiltering.ViewModel
{
    internal class ViewModel : BaseNotifyPropertyChanged
    {
        private string filterElapsed = "...";
        public string FilterElapsed { get => filterElapsed; set => Set(ref filterElapsed, value); }

        private Visibility filterVisibility;
        public Visibility FilterVisibility { get => filterVisibility; set => Set(ref filterVisibility, value); }

        private string filterText;
        public string FilterText { get => filterText; set { Set(ref filterText, value); OnFilterTextChanged(); } }

        private ObservableCollection<Node> Nodes { get; } = new ObservableCollection<Node>();
        public ListCollectionView TreeViewItems { get; set; }

        public ViewModel()
        {
            TreeViewItems = new ListCollectionView(Nodes);

            Task.Run(() =>
            {
                var timer = new Stopwatch();
                timer.Start();

                for (var i1 = 0; i1 < 10; i1++)
                {
                    var node1 = new Node($"ROOT #{i1}");
                    for (var i2 = 0; i2 < 10; i2++)
                    {
                        var node2 = new Node($"Children Level 1 FEE #{i2}");
                        node1.AddChild(node2);
                        for (var i3 = 0; i3 < 10; i3++)
                        {
                            var node3 = new Node($"Children Level 2 FOO #{i3}");
                            node2.AddChild(node3);
                            for (var i4 = 0; i4 < 10; i4++)
                            {
                                var node4 = new Node($"Children Level 3 GGG #{i4}");
                                node3.AddChild(node4);
                                for (var i5 = 0; i5 < 10; i5++)
                                {
                                    var node5 = new Node($"Children Level 4 MMM #{i5}");
                                    node4.AddChild(node5);
                                    for (var i6 = 0; i6 < 10; i6++)
                                    {
                                        var node6 = new Node($"Children Level 5 Joker #{i6}");
                                        node5.AddChild(node6);
                                    }
                                }
                            }
                        }
                    }

                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        Nodes.Add(node1);
                    });
                }

                FilterElapsed = $"Initialization: {timer.Elapsed.ToString()}";
                FilterVisibility = Visibility.Collapsed;
            });
        }

        private void OnFilterTextChanged()
        {
            FilterVisibility = Visibility.Visible;
            Task.Run(() =>
            {
                var timer = new Stopwatch();
                timer.Start();

                Application.Current.Dispatcher.Invoke(() =>
                {
                    TreeViewItems.Filter = obj => Node.ShowNode(FilterText, (Node)obj);
                });

                FilterElapsed = $"Elapsed: {timer.Elapsed.ToString()}";
                FilterVisibility = Visibility.Collapsed;
            });
        }

    }
}

using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace TableReservation.Utilities
{
    public class CustomBoundColumn : DataGridTemplateColumn
    {
        public DataTemplate AttachedCellTemplate { get; set; }
        public DataTemplate AttachedCellEditingTemplate { get; set; }
        public MappedValueCollection MappedValueCollection { get; set; }
       
        protected override FrameworkElement GenerateElement(DataGridCell cell, object dataItem)
        {
            var content = new ContentControl();
            MappedValue context = MappedValueCollection.ReturnIfExistAddIfNot(cell.Column.Header, dataItem);
            var binding = new Binding() { Source = context };
            content.ContentTemplate = cell.IsEditing ? CellEditingTemplate : CellTemplate;
            content.SetBinding(ContentControl.ContentProperty, binding);           
            return content;
        }

        protected override FrameworkElement GenerateEditingElement(DataGridCell cell, object dataItem)
        {
            return GenerateElement(cell, dataItem);
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;

namespace PropertyGridTest
{
    class MyFunkyTypeConverter : ExpandableObjectConverter
    {
        public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributes)
        {
            PropertyDescriptorCollection props = base.GetProperties(context, value, attributes);
            List<PropertyDescriptor> list = new List<PropertyDescriptor>(props.Count);
            foreach (PropertyDescriptor prop in props)
            {
                switch (prop.Name)
                {
                    case "Distance":
                        list.Add(new DisplayNamePropertyDescriptor(
                            prop, "your magic code here"));
                        break;
                    default:
                        list.Add(prop);
                        break;
                }
            }
            return new PropertyDescriptorCollection(list.ToArray(), true);
        }
    }
    class DisplayNamePropertyDescriptor : PropertyDescriptor
    {
        private readonly string displayName;
        private readonly PropertyDescriptor parent;
        public DisplayNamePropertyDescriptor(
            PropertyDescriptor parent, string displayName) : base(parent)
        {
            this.displayName = displayName;
            this.parent = parent;
        }
        public override string DisplayName
        { get { return displayName; } }

        public override bool ShouldSerializeValue(object component)
        { return parent.ShouldSerializeValue(component); }

        public override void SetValue(object component, object value)
        {
            parent.SetValue(component, value);
        }
        public override object GetValue(object component)
        {
            return parent.GetValue(component);
        }
        public override void ResetValue(object component)
        {
            parent.ResetValue(component);
        }
        public override bool CanResetValue(object component)
        {
            return parent.CanResetValue(component);
        }
        public override bool IsReadOnly
        {
            get { return parent.IsReadOnly; }
        }
        public override void AddValueChanged(object component, EventHandler handler)
        {
            parent.AddValueChanged(component, handler);
        }
        public override void RemoveValueChanged(object component, EventHandler handler)
        {
            parent.RemoveValueChanged(component, handler);
        }
        public override bool SupportsChangeEvents
        {
            get { return parent.SupportsChangeEvents; }
        }
        public override Type PropertyType
        {
            get { return parent.PropertyType; }
        }
        public override TypeConverter Converter
        {
            get { return parent.Converter; }
        }
        public override Type ComponentType
        {
            get { return parent.ComponentType; }
        }
        public override string Description
        {
            get { return parent.Description; }
        }
        public override PropertyDescriptorCollection GetChildProperties(object instance, Attribute[] filter)
        {
            return parent.GetChildProperties(instance, filter);
        }
        public override string Name
        {
            get { return parent.Name; }
        }

    }

    [TypeConverter(typeof(MyFunkyTypeConverter))]
    class MyFunkyType
    {
        public double Distance { get; set; }

        public double AnotherProperty { get; set; }
    }
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.Run(new Form
            {
                Controls = {
                    new PropertyGrid { Dock = DockStyle.Fill,
                        SelectedObject = new MyFunkyType {
                            Distance = 123.45
                }}
            }
            });
        }
    }
}

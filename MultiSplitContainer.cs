using System.Windows.Forms;
using System.Collections.Generic;

class MultiSplitContainer : SplitContainer
{
    private List<Control> _controls;

    public List<Control> controls
    {
        get { return _controls; }
        set
        {
            if (value != null)
            {
                if (value.Count >= 2)
                {
                    _controls = value;
                }
                else
                {
                    throw new System.Exception("Multi split needs atleast 2 controls");
                }
            }
        }
    }

    public List<SplitContainer> childContainers = new List<SplitContainer>();

    public void Init()
    {
        int controlCount = _controls.Count;

        this.Panel1.Controls.Add(_controls[0]);
        this.Panel2.Controls.Add(_controls[1]);

        SplitContainer prevContainer = this;

        for (int i = 2; i < controlCount; i++)
        {
            SplitContainer container = new SplitContainer();
            container.Dock = DockStyle.Fill;
            container.Orientation = this.Orientation;
            container.SplitterWidth = this.SplitterWidth;
            container.Panel1.Controls.Add(prevContainer.Panel2.Controls[0]);
            container.Panel2.Controls.Add(_controls[i]);
            prevContainer.Panel2.Controls.Clear();
            prevContainer.Panel2.Controls.Add(container);
            prevContainer = container;
            childContainers.Add(container);
        }
    }

    public SplitContainer GetChildContainer(int index)
    {
        return childContainers[index];
    }

    public Panel GetPanel(int index)
    {
        if (index == 0)
        {
            return this.Panel1;
        }
        else
        {
            return childContainers[index].Panel2;
        }
    }

}

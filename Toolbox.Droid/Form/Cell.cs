﻿using Android.Content;
using Android.OS;
using Android.Views;

namespace Toolbox.Droid.Form
{
    public abstract class Cell
    {
        protected readonly Bundle _configuration;

        public readonly int Tag;

        /// <summary>
        ///     Base constructor for the cell object.
        /// </summary>
        /// <param name="tag">A unique Identifier</param>
        /// <param name="form">The FormFragmentObject that will display the cell</param>
        public Cell(int tag, FormFragment form)
        {
            Tag = tag;
            Form = form;
            _configuration = new Bundle();
            Form.EditableStatusChanged += OnEditableStatusChanged;
        }

        protected FormFragment Form { get; }

        /// <summary>
        ///     Override this methode to provide a visual content for the cell
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public abstract View GetView(Context context);

        /// <summary>
        ///     Call this method when the cell needs to informs the form and other subscribees that it's content as changed.
        /// </summary>
        /// <param name="newValue"></param>
        /// <typeparam name="T"></typeparam>
        protected void NotifyChanged(object newValue)
        {
            Form.OnCellDataChanged(this, newValue);
        }

        /// <summary>
        ///     React to cell being unselected
        /// </summary>
        /// <param name="c"></param>
        public virtual void OnLoosingFocus(Context c)
        {
        }

        /// <summary>
        ///     Reacted to cell being clicked on
        /// </summary>
        /// <param name="c"></param>
        public virtual void OnGainingFocus(Context c)
        {
        }

        /// <summary>
        ///     Override to establish custom behavior according when mode change from readwrite to readonly
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void OnEditableStatusChanged(object sender, bool isEditable)
        {
        }
    }
}
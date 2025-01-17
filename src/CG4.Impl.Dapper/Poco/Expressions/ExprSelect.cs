﻿using System.Collections;

namespace CG4.Impl.Dapper.Poco.Expressions
{
    public class ExprSelect : Expr, IEnumerable<Expr>
    {
        private readonly List<Expr> _selectedColumns = new();

        public void Add(Expr selectedColumn)
        {
            _selectedColumns.Add(selectedColumn);
        }

        public override void Accept(IExprVisitor visitor) => visitor.VisitSelect(this);

        public IEnumerator<Expr> GetEnumerator()
        {
            return _selectedColumns.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _selectedColumns.GetEnumerator();
        }
    }
}

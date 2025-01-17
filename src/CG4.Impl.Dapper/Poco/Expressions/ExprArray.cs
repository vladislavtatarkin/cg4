﻿using System.Collections;

namespace CG4.Impl.Dapper.Poco.Expressions
{
    public class ExprArray : ExprConst, IEnumerable<Expr>
    {
        private readonly List<Expr> _values = new();

        public void Add(Expr expr)
        {
            _values.Add(expr);
        }

        public override void Accept(IExprVisitor visitor) => visitor.VisitArray(this);

        public IEnumerator<Expr> GetEnumerator()
        {
            return _values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _values.GetEnumerator();
        }
    }
}

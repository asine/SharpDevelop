﻿// Copyright (c) AlphaSierraPapa for the SharpDevelop Team (for details please see \doc\copyright.txt)
// This code is distributed under the GNU LGPL (for details please see \doc\license.txt)
using System;
using System.Collections.ObjectModel;
using ICSharpCode.Reporting.Expressions;
using ICSharpCode.Reporting.Expressions.Irony;
using ICSharpCode.Reporting.Expressions.Irony.Ast;
using ICSharpCode.Reporting.Items;
using ICSharpCode.Reporting.PageBuilder.ExportColumns;

namespace ICSharpCode.Reporting.Exporter.Visitors
{
	/// <summary>
	/// Description of ExpressionVisitor.
	/// </summary>
	class ExpressionVisitor: AbstractVisitor
	{
		readonly Collection<ExportPage> pages;
		readonly ReportingLanguageGrammer grammar;
		readonly ReportingExpressionEvaluator evaluator;
		
		
		public ExpressionVisitor(Collection<ExportPage> pages,ReportSettings reportSettings):this(reportSettings)
		{
			this.pages = pages;
		}
		
		
		internal ExpressionVisitor(ReportSettings reportSettings) {
			grammar = new ReportingLanguageGrammer();
			evaluator = new ReportingExpressionEvaluator(grammar);
			evaluator.AddReportSettings(reportSettings);
		}
		
		
		public override void Visit(ExportPage page)
		{
			evaluator.AddPageInfo(page.PageInfo);
			foreach (var element in page.ExportedItems) {
				var ac = element as IAcceptor;
				ac.Accept(this);
			}
		}
		
		
		public override void Visit(ExportContainer exportContainer)
		{
			evaluator.AddCurrentContainer(exportContainer);
			foreach (var element in exportContainer.ExportedItems) {
				var ac = element as IAcceptor;
				ac.Accept(this);
			}
		}
		
		
		public override void Visit(ExportText exportColumn)
		{
			if (ExpressionHelper.CanEvaluate(exportColumn.Text)) {
				try {
					object result = Evaluate(exportColumn);
					exportColumn.Text = result.ToString();
				} catch (Exception e) {
					var s = String.Format("SharpReport.Expressions -> {0} for {1}",e.Message,exportColumn.Text);
					Console.WriteLine(s);
				}
			}
		}

		
		object Evaluate(ExportText exportColumn)
		{
			var str = ExpressionHelper.ExtractExpressionPart(exportColumn.Text);
			var result = evaluator.Evaluate(str);
			return result;
		}
		
		public ReportingExpressionEvaluator Evaluator {
			get { return evaluator; }
		}
		
	}
}
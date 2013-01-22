﻿using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using Ikon.Utilities;

namespace Ikon
{
	/// <summary>
	/// Base class for IKON composers. Composers transform IKON values to
	/// a text.
	/// </summary>
	public class IkonWriter
	{
		/// <summary>
		/// Output stream where IKON values are being written.
		/// </summary>
		protected TextWriter Writer { get; private set; }

		/// <summary>
		/// Indentation level.
		/// </summary>
		public Indentation Indentation { get; protected set; }

		/// <summary>
		/// Temporary line contents.
		/// </summary>
		protected StringBuilder Line { get; private set; }

		/// <summary>
		/// Constructs basic IKON composer.
		/// </summary>
		/// <param name="writer">Output stream.</param>
		public IkonWriter(TextWriter writer)
		{
			this.Writer = writer;

			this.Line = new StringBuilder();
			this.Indentation = new Indentation();
		}
	
		/// <summary>
		/// Appends a text to the current line. Text entered with this method
		/// is buffered and is not written immediately to the output stream.
		/// To finalize the buffered data (and write it to output stream) call
		/// either EndLine or WrtieLine.
		/// </summary>
		/// <param name="text">Raw text.</param>
		public void Write(string text)
		{
			if (text == null)
				throw new ArgumentNullException("text");

			Line.Append(text);
		}

		/// <summary>
		/// Appends a text to the current line and writes buffered line to the
		/// output stream.
		/// </summary>
		/// <param name="text">Raw text.</param>
		public void WriteLine(string text)
		{
			if (text == null)
				throw new ArgumentNullException("text");

			Line.Append(text);
			EndLine();
		}

		/// <summary>
		/// Writes reference names to the output stream.
		/// </summary>
		/// <param name="referenceNames">List of reference names.</param>
		public void WriteReferences(ICollection<string> referenceNames)
		{
			if (referenceNames == null)
				throw new ArgumentNullException("referenceNames");

			foreach (string name in referenceNames)
				Write(" " + IkonReader.ReferenceSign + name);
		}

		/// <summary>
		/// Writes buffered line to the output stream.
		/// </summary>
		public void EndLine()
		{
			if (Line.Length > 0)
			{
				Writer.Write(Indentation);
				Writer.WriteLine(Line);
				Line.Clear();
			}
		}
	}
}
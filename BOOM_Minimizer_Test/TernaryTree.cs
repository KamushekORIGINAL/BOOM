﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOOM_Minimizer_Test {

	public class TernaryTree: IDisposable {
		/*
		*    node
		*   / | \
		*  0  -  1 
		*/

		public void Dispose() {
			root=null;
			GC.Collect();
		}

		private TernaryNode root;

		private int length;

		public int Count { get; set; }

		private List<string> InOrder(string history, TernaryNode node, int depth) {
			if (node==null)
				return new List<string>();
			if (depth==length)
				return new List<string>() { history };

			List<string> result = new List<string>();
			result.AddRange(InOrder(history+"0", node.Left, depth+1));
			result.AddRange(InOrder(history+"-", node.Middle, depth+1));
			result.AddRange(InOrder(history+"1", node.Right, depth+1));
			return result;
		}

		public string[] GetElements() {
			return InOrder("", root, 0).ToArray();
		}

		/// <summary>
		/// Добавляет элемент в дерево
		/// </summary>
		/// <param name="input">Входная строка</param>
		/// <returns>true если элемент ранее не содержался</returns>
		public bool Add(string input) {
			if (input.Length != length)
				throw new ArgumentException(String.Format("Строка имеет длинну {0}, а должна быть длинной в {1} символов.", input.Length, length));
			bool result = false;
			TernaryNode buffer = root;
			foreach (char a in input) {
				switch (a) {
				case '0':
					if (buffer.Left == null) {
						result = true;
						buffer.Left = new TernaryNode();
					}
					buffer = buffer.Left;
					break;
				case '-':
					if (buffer.Middle == null) {
						result = true;
						buffer.Middle = new TernaryNode();
					}
					buffer = buffer.Middle;
					break;
				case '1':
					if (buffer.Right == null) {
						result = true;
						buffer.Right = new TernaryNode();
					}
					buffer = buffer.Right;
					break;
				default:
					throw new ArgumentException("Строка иммет неверный символ.");
				}
			}
			if (result)
				Count++;
			return result;
		}

		public bool Remove(string input) {
			if (input.Length != length)
				throw new ArgumentException(String.Format("Строка имеет длинну {0}, а должна быть длинной в {1} символов.", input.Length, length));
			long depth = 1;
			TernaryNode buffer = root;
			foreach (char c in input) {
				switch (c) {
				case '0':
					if (buffer.Left == null)
						throw new ArgumentException("Строка не содержится.");
					buffer = buffer.Left;
					if (depth == length - 1) {
						buffer.Left = null;
						Count--;
						return true;
					}
					break;
				case '-':
					if (buffer.Middle == null)
						throw new ArgumentException("Строка не содержится.");
					buffer = buffer.Middle;
					if (depth == length - 1) {
						buffer.Middle = null;
						Count--;
						return true;
					}
					break;
				case '1':
					if (buffer.Right == null)
						throw new ArgumentException("Строка не содержится.");
					buffer = buffer.Right;
					if (depth == length - 1) {
						buffer.Right = null;
						Count--;
						return true;
					}
					break;
				default:
					throw new ArgumentException("Строка содержит недопустимые символы.");
				}
				depth++;
			}
			return false;
		}

		public bool Contains(string input) {
			if (input.Length != length)
				throw new ArgumentException(String.Format("Строка имеет длинну {0}, а должна быть длинной в {1} символов.", input.Length, length));
			long depth = 1;
			TernaryNode buffer = root;
			foreach (char c in input) {
				switch (c) {
				case '0':
					if (buffer.Left == null)
						return false;
					buffer = buffer.Left;
					if (depth == length)
						return true;
					break;
				case '-':
					if (buffer.Middle == null)
						return false;
					buffer = buffer.Middle;
					if (depth == length)
						return true;
					break;
				case '1':
					if (buffer.Right == null)
						return false;
					buffer = buffer.Right;
					if (depth == length)
						return true;
					break;
				default:
					throw new ArgumentException("Строка содержит недопустимые символы.");
				}
				depth++;
			}
			return false;
		}

		public TernaryTree(int _length) {
			length = _length;
			Count = 0;
			root = new TernaryNode();
		}

	}

	public class TernaryNode {

		public TernaryNode Left { get; set; }
		public TernaryNode Middle { get; set; }
		public TernaryNode Right { get; set; }

		public TernaryNode() {
			Left = null;
			Middle = null;
			Right = null;
		}


	}

}

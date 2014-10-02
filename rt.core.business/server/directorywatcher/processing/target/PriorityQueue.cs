// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PriorityQueue.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

#region



#endregion

namespace rt.core.business.server.directorywatcher.processing.target
{
  using System.Collections.Generic;
  using System.Linq;

  /// <summary>
  /// The priority queue.
  /// </summary>
  /// <typeparam name="T">
  /// Тип эелемента
  /// </typeparam>
  public class PriorityQueue<T>
  {
    /// <summary>
    ///   The storage.
    /// </summary>
    private readonly SortedDictionary<int, Queue<T>> storage;

    /// <summary>
    ///   The total_size.
    /// </summary>
    private int totalSize;

    /// <summary>
    ///   Initializes a new instance of the <see>
    ///                                       <cref>PriorityQueue</cref>
    ///                                     </see>  class.
    /// </summary>
    public PriorityQueue()
    {
      storage = new SortedDictionary<int, Queue<T>>();
      totalSize = 0;
    }

    /// <summary>
    /// Gets the count.
    /// </summary>
    public int Count
    {
      get
      {
        return storage.Values.Sum(x => x.Count);
      }
    }

    /// <summary>
    ///   The is empty.
    /// </summary>
    /// <returns> The <see cref="bool" /> . </returns>
    public bool IsEmpty()
    {
      return totalSize == 0;
    }

    /// <summary>
    ///   The dequeue.
    /// </summary>
    /// <returns> The <see cref="object" /> . </returns>
    public T Dequeue()
    {
      foreach (var q in storage.Values.Where(q => q.Count > 0))
      {
        totalSize--;
        return q.Dequeue();
      }

      return default(T);
    }

    // same as above, except for peek.

    /// <summary>
    ///   The peek.
    /// </summary>
    /// <returns> The <see cref="object" /> . </returns>
    public object Peek()
    {
      return storage.Values.Where(q => q.Count > 0).Select(q => q.Peek()).FirstOrDefault();
    }

    /// <summary>
    /// The dequeue.
    /// </summary>
    /// <param name="prio">
    /// The prio. 
    /// </param>
    /// <returns>
    /// The <see cref="object"/> . 
    /// </returns>
    public T Dequeue(int prio)
    {
      totalSize--;
      return storage[prio].Dequeue();
    }

    /// <summary>
    /// The enqueue.
    /// </summary>
    /// <param name="item">
    /// The item. 
    /// </param>
    /// <param name="prio">
    /// The prio. 
    /// </param>
    public void Enqueue(T item, int prio)
    {
      if (!storage.ContainsKey(prio))
      {
        storage.Add(prio, new Queue<T>());
        Enqueue(item, prio);
      }
      else
      {
        storage[prio].Enqueue(item);
        totalSize++;
      }
    }
  }
}
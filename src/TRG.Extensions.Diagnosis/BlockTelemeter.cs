namespace TRG.Extensions.Diagnosis
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;

    public class BlockTelemeter : IDisposable
    {
        private readonly string blockName;
        private readonly Action<string> writeAction;
        private readonly bool writeNewLine;
        private readonly List<CheckPoint> collection;
        private readonly Stopwatch stopwatch;

        public BlockTelemeter(string blockName, TextWriter textWriter)
            : this(blockName, textWriter.WriteLine, false)
        {
            if (textWriter == null) throw new ArgumentNullException(nameof(textWriter));
        }

        public BlockTelemeter(string blockName, Action<string> writeAction, bool writeNewLine = true)
        {
            this.blockName = blockName;
            this.writeAction = writeAction ?? throw new ArgumentNullException(nameof(writeAction));
            this.writeNewLine = writeNewLine;
            this.collection = new List<CheckPoint>();
            this.stopwatch = Stopwatch.StartNew();
        }

        public void Snap(string checkpointName)
        {
            this.RecordTime(checkpointName);

#if NET35
            _stopwatch.Reset();
            _stopwatch.Start();
#else
            this.stopwatch.Restart();
#endif
        }

        public void Dispose()
        {
            this.RecordTime("[End]");
            this.stopwatch.Stop();

            var average = this.collection.Average(i => i.Time);
            var total = this.collection.Sum(i => i.Time);

            var newLine = this.writeNewLine ? Environment.NewLine : null;
            this.writeAction($"[{this.blockName}] {this.collection.Count} checkpoints in {total:N0}ms (Average: {average:N0}ms).{newLine}");

            for (var i = 0; i < this.collection.Count; i++)
            {
                var checkPoint = this.collection[i];

                this.writeAction($"[{this.blockName}] {checkPoint.Name}:{i + 1} - {checkPoint.Time:N0}ms{newLine}");
            }
        }

        private void RecordTime(string name)
        {
            var time = this.stopwatch.ElapsedMilliseconds;
            this.collection.Add(new CheckPoint(name, time));
        }

        private class CheckPoint
        {
            public string Name { get; }
            public long Time { get; }

            public CheckPoint(string name, long time)
            {
                this.Name = name;
                this.Time = time;
            }
        }
    }
}
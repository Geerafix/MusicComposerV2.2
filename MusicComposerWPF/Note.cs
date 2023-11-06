using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicComposer {
    public class Note {
        int number;
        int duration;

        public Note(int number, int duration) {
            this.number = number;
            this.duration = duration;
        }

        public int getNumber() {
            return this.number;
        }

        public int getDuration() {
            return this.duration;
        }

        public void setNumber(int number) {
            this.number = number;
        }

        public void setDuration(int duration) {
            this.duration = duration;
        }
    }
}

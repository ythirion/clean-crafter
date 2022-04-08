import java.util.ArrayList;
import java.util.List;

public class UglyPoker {

        public String card1;
        public String card2;
        public String card3;
        public String card4;
        public String card5;

        public UglyPoker(String p1, String p2, String p3, String p4, String p5) {
            this.card1 = p1;
            this.card2 = p2;
            this.card3 = p3;
            this.card4 = p4;
            this.card5 = p5;
        }

        public boolean doCompareHC(String p1, String p2, String p3, String p4, String p5) {
            String hc = "s02";
            List<String> o = new ArrayList<String>();
            List<String> m = new ArrayList<String>();
            o.add(p1);
            o.add(p2);
            o.add(p3);
            o.add(p4);
            o.add(p5);
            m.add(card1);
            m.add(card2);
            m.add(card3);
            m.add(card4);
            m.add(card5);

            for (int i = 0; i < o.size(); i++) {
                String mc = m.get(i);
                for (int j = 0; j < o.size(); j++) {
                    String oc = m.get(j);
                    if (Integer.valueOf(oc.substring(1)) > Integer.valueOf(mc.substring(1))) {
                        if (Integer.valueOf(oc.substring(1)) > Integer.valueOf(hc.substring(1))) {
                            hc = oc;
                        }
                    } else {
                        if (Integer.valueOf(mc.substring(1)) > Integer.valueOf(hc.substring(1))) {
                            hc = mc;
                        }
                    }
                }
            }
            return m.contains(hc);
        }
    }

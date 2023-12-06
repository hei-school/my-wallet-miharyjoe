import java.text.SimpleDateFormat;
import java.util.Date;

class Transaction {
  private Date date;
  private double amount;
  private String status;

  public Transaction(double amount, String status) {
    this.date = new Date();
    this.amount = amount;
    this.status = status;
  }

  @Override
  public String toString() {
    SimpleDateFormat sdf = new SimpleDateFormat("yyyy-MM-dd HH:mm:ss");
    return String.format("| %-20s | %-10.2f | %-15s |", sdf.format(date), amount, status);
  }
}
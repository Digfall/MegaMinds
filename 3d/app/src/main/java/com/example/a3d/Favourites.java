package com.example.a3d;

import androidx.appcompat.app.AppCompatActivity;

import android.content.Intent;
import android.os.Bundle;
import android.view.Menu;
import android.view.MenuItem;
import android.view.View;

public class Favourites extends AppCompatActivity {
    @Override
    public boolean onOptionsItemSelected(MenuItem item)
    {
        if (item.getItemId() == R.id.item2)
        {
            Intent intent = new Intent(this, Main.class);
            startActivity(intent);

        } else if (item.getItemId() == R.id.item3)
        {
            Intent intent = new Intent(this, Favourites.class);
            startActivity(intent);

            {
                return super.onOptionsItemSelected(item);
            }

        }
        return true;
    }
    @Override
    public boolean onCreateOptionsMenu(Menu menu) {
        getMenuInflater().inflate(R.menu.example_menu, menu);
        return true;
    }
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_favourites);
    }
    public void startNewActivity(View v){
        Intent intent = new Intent(this,  Main.class);
        startActivity(intent);
    }
    public void startNewActivity2(View v){
        Intent intent2 = new Intent(this, Favourites.class);
        startActivity(intent2);
    }
}
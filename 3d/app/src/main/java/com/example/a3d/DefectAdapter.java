package com.example.a3d;

import android.app.Activity;
import android.content.Context;
import android.graphics.Bitmap;
import android.graphics.BitmapFactory;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ImageView;
import android.widget.TextView;

import androidx.annotation.NonNull;
import androidx.constraintlayout.widget.ConstraintLayout;
import androidx.recyclerview.widget.RecyclerView;

import java.util.ArrayList;

public class DefectAdapter extends RecyclerView.Adapter<DefectAdapter.DefectViewHolder> {
    private Context context;
    private Activity activity;
    private ArrayList defect_title, defect_image, defect_description, defect_causes, defect_solutions;


    DefectAdapter(Activity activity, Context context, ArrayList defect_title, ArrayList defect_image, ArrayList defect_description) {
        this.activity = activity;
        this.context = context;
        this.defect_title = defect_title;
        this.defect_image = defect_image;
        this.defect_description = defect_description;
//        this.defect_causes = defect_causes;
//        this.defect_solutions = defect_solutions;
    }
    @NonNull
    @Override
    public DefectViewHolder onCreateViewHolder(@NonNull ViewGroup parent, int viewType) {
        LayoutInflater inflater = LayoutInflater.from(context);
        View view = inflater.inflate(R.layout.defect_item, parent, false);
        return new DefectViewHolder(view);
    }


    @Override
    public void onBindViewHolder(@NonNull final DefectViewHolder holder, final int position) {
        holder.defect_title.setText(String.valueOf(defect_title.get(position)));
        holder.defect_description.setText(String.valueOf(defect_description.get(position)));
        byte[] image = (byte[]) defect_image.get(position);
        Bitmap bitmap = BitmapFactory.decodeByteArray(image, 0, image.length);
        holder.defect_image.setImageBitmap(bitmap);
    }

    @Override
    public int getItemCount() {
        return defect_title.size();
    }
    class DefectViewHolder extends RecyclerView.ViewHolder {
        ConstraintLayout mainLayout;
        ImageView defect_image;
        TextView defect_title, defect_description;

        DefectViewHolder(@NonNull View itemView) {
            super(itemView);
            defect_image = itemView.findViewById(R.id.defect_image);
            defect_title = itemView.findViewById(R.id.defect_title);
            defect_description = itemView.findViewById(R.id.defect_description);
            mainLayout = itemView.findViewById(R.id.mainLayout);

        }

    }
}
